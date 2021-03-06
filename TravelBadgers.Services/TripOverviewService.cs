using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TravelBadgers.Data;
using TravelBadgers.Models;

namespace TravelBadgers.Services
{
    public class TripOverviewService
    {
        private readonly Guid _userId;

        public TripOverviewService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<SelectListItem> RequestsList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                IEnumerable<SelectListItem> requestList = ctx.Requests.Select(Request => new SelectListItem
                {
                    Text = Request.RequestId.ToString(),
                    Value = Request.RequestId.ToString()
                });
                return requestList.ToArray();
            }
        }

        public Request GetRequestById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx
                        .Requests
                        .Single(e => e.RequestId == id);

            }
        }

        public bool CreateTripOverview(Request model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<City> cities = ctx.Cities.ToList();
                City arrivingCity = ctx.Cities.Single(e => e.CityId == model.CityId);
                List<City> citiesWithDepartRemoved = cities;
                citiesWithDepartRemoved.Remove(arrivingCity);
                List<decimal> flightCosts = new List<decimal>();
                List<decimal> overallCosts = new List<decimal>();
                City departing = new City();

                decimal daysToSpend = (decimal)model.DepartDate.Subtract(model.ReturnDate).TotalDays;


                foreach (City x in citiesWithDepartRemoved)
                {

                    double radiansToDegrees = Math.PI / 180;
                    double departingLocationNorthConv = decimal.ToDouble(departing.LocationNorth);
                    double departingLocationWestConv = decimal.ToDouble(departing.LocationWest);
                    double arrivalLocationNorthConv = decimal.ToDouble(x.LocationNorth);
                    double arrivalLocationWestConv = decimal.ToDouble(x.LocationWest);

                    double sinNorthDeparting = Math.Sin(departingLocationNorthConv * radiansToDegrees);
                    double sinNorthArriving = Math.Sin(arrivalLocationNorthConv * radiansToDegrees);
                    double cosNorthDeparting = Math.Cos(departingLocationNorthConv * radiansToDegrees);
                    double cosNorthArriving = Math.Cos(arrivalLocationNorthConv * radiansToDegrees);

                    double departingnWestDegrees = departingLocationWestConv * radiansToDegrees;
                    double arrivingWestDegrees = arrivalLocationWestConv * radiansToDegrees;

                    double cosWest = Math.Cos(departingnWestDegrees - arrivingWestDegrees);
                    double multCosines = cosNorthDeparting * cosNorthArriving * cosWest;
                    double multSines = sinNorthDeparting * sinNorthArriving;

                    double aCosineOverall = Math.Acos(multSines + multCosines);

                    double distanceCalcReturn = 3443.8985 * aCosineOverall * 2;

                    decimal overallDistance = (decimal)distanceCalcReturn;

                    flightCosts.Add(overallDistance);

                    decimal overallEntertainment = x.AvgEntertainmentDaily * daysToSpend;
                    decimal overallHotel = x.AvgHotelDailyCost * daysToSpend;
                    decimal overallFood = x.AvgFoodDaily * daysToSpend;
                    decimal overallCostOfTrip = overallDistance + overallEntertainment + overallHotel + overallFood;

                    overallCosts.Add(overallCostOfTrip);

                };

                int counter = 0;
                List<City> citiesWithinBugdet = new List<City>();
                List<decimal> flightCostsWithinBugdet = new List<decimal>();
                List<decimal> overallCostsWithinBugdet = new List<decimal>();

                foreach (City arriving in citiesWithDepartRemoved)
                {
                    if(overallCosts[counter] <= model.OverallBudget)
                    {
                        citiesWithinBugdet.Add(arriving);
                        flightCostsWithinBugdet.Add(flightCosts[counter]);
                        overallCostsWithinBugdet.Add(overallCosts[counter]);
                    }

                    counter = counter + 1;
                }

                var trip =
                    new TripOverview()
                    {
                        OwnerId = _userId,
                        RequestId = model.RequestId,
                        CitiesWithinBudget = citiesWithinBugdet,
                        CreatedUtc = DateTimeOffset.UtcNow
                    };

                ctx.TripOverviews.Add(trip);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TripOverviewListItem> GetTripOverviews()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TripOverviews
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            request =>
                                new TripOverviewListItem
                                {
                                    TripOverviewId = request.TripOverviewId,
                                    DepartCityId = request.Request.CityId,
                                    //ArrivalCityId = request.ArrivalCityId,
                                    //OverallCost = request.OverallCost,
                                    CreatedUtc = request.CreatedUtc
                                }
                       );

                return query.ToArray();
            }
        }

        public TripOverviewDetail GetTripOverviewById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TripOverviews
                        .Single(e => e.TripOverviewId == id && e.OwnerId == _userId);
                return
                    new TripOverviewDetail
                    {
                        TripOverviewId = entity.TripOverviewId,
                        DepartCityId = entity.Request.CityId,
                        RequestId = entity.RequestId,
                        ArrivalCityId = entity.CitiesWithinBudget,
                        //FlightCost = entity.FlightCost,
                        //OverallCost = entity.OverallCost,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool DeleteTripOverview(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TripOverviews
                        .Single(e => e.TripOverviewId == id && e.OwnerId == _userId);

                ctx.TripOverviews.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
