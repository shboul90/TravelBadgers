using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public bool CreateTripOverview(TripOverviewCreate model)
        {

            var entity =
            new TripOverview()
            {
                OwnerId = _userId,
                RequestId = model.RequestId,
                ArrivalCityId = model.ArrivalCityId,
                FlightCost = model.FlightCost,
                OverallCost = model.OverallCost,
                CreatedUtc = DateTimeOffset.UtcNow
            };

            using (var ctx = new ApplicationDbContext())
            {

                ctx.TripOverviews.Add(entity);
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
                                    ArrivalCityId = request.ArrivalCityId,
                                    OverallCost = request.OverallCost,
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
                        ArrivalCityId = entity.ArrivalCityId,
                        FlightCost = entity.FlightCost,
                        OverallCost = entity.OverallCost,
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
