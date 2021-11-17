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
    public class TripService
    {
        private readonly Guid _userId;

        public TripService(Guid userId)
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

        public bool CreateTrip(Request model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<City> cities = ctx.Cities.ToList();
                City arrivingCity = ctx.Cities.Single(e => e.CityId == model.CityId);
                List<City> citiesWithDepartRemoved = cities;
                citiesWithDepartRemoved.Remove(arrivingCity);
                List<decimal> flightCosts = new List<decimal>();
                List<decimal> overallCosts = new List<decimal>();
                City departing = arrivingCity;

                decimal daysToSpend = (decimal)model.ReturnDate.Subtract(model.DepartDate).TotalDays;


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
                    double addSinCos = multSines + multCosines;

                    double aCosineOverall = Math.Acos(addSinCos);

                    double distanceCalcReturn = 3443.8985 * aCosineOverall * 2 * 0.4;

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
                    if (overallCosts[counter] <= model.OverallBudget)
                    {
                        citiesWithinBugdet.Add(arriving);
                        flightCostsWithinBugdet.Add(flightCosts[counter]);
                        overallCostsWithinBugdet.Add(overallCosts[counter]);
                    }

                    counter = counter + 1;
                }

                int counter2 = 0;

                foreach (City tripsAvailable in citiesWithinBugdet )
                {
                  var trips =
                    new Trip()
                    {
                        OwnerId = _userId,
                        RequestId = model.RequestId,
                        ArrivalCity = tripsAvailable,
                        FlightCost = flightCostsWithinBugdet[counter2],
                        OverallCost = overallCostsWithinBugdet[counter2]
                    };

                    ctx.Trips.Add(trips);

                    counter2 = counter2 + 1;
                }
                
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<TripListItem> GetTrips()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Trips
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            request =>
                                new TripListItem
                                {
                                    TripId = request.TripId,
                                    RequestId = request.RequestId,
                                    DepartCityName = request.Request.City.CityName,
                                    ArrivalCityName = request.ArrivalCity.CityName,
                                    OverallCost = request.OverallCost
                                }
                       );

                return query.ToArray();
            }
        }

        public TripDetail GetTripById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Trips
                        .Single(e => e.TripId == id && e.OwnerId == _userId);
                return
                    new TripDetail
                    {
                        TripId = entity.TripId,
                        RequestId = entity.RequestId,
                        DepartCityName = entity.Request.City.CityName,
                        ArrivalCityName = entity.ArrivalCity.CityName,
                        FlightCost = entity.FlightCost,
                        OverallCost = entity.OverallCost
                    };
            }
        }

        public bool DeleteTrip(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Trips
                        .Single(e => e.TripId == id && e.OwnerId == _userId);

                ctx.Trips.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
