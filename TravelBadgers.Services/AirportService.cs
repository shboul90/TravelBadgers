using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;
using TravelBadgers.Models;

namespace TravelBadgers.Services
{
    public class AirportService
    {
        private readonly Guid _userId;

        public AirportService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAirport(AirportCreate model)
        {
            var entity =
                new Airport()
                {
                    OwnerId = _userId,
                    AirportName = model.AirportName,
                    AirpotZipCode = model.AirpotZipCode,
                    LocationNorth = model.LocationNorth,
                    LocationWest = model.LocationWest,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Airports.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AirportListItem> GetAirports()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Airports
                        .Select(
                            airport =>
                                new AirportListItem
                                {
                                    AirportId = airport.AirportId,
                                    AirportName = airport.AirportName,
                                    AirpotZipCode = airport.AirpotZipCode,
                                }
                       );

                return query.ToArray();
            }
        }

        public AirportDetail GetAirportById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Airports
                        .Single(e => e.AirportId == id);
                return
                    new AirportDetail
                    {
                        AirportId = entity.AirportId,
                        AirportName = entity.AirportName,
                        AirpotZipCode = entity.AirpotZipCode,
                        LocationNorth = entity.LocationNorth,
                        LocationWest = entity.LocationWest,
                    };
            }
        }

        public bool UpdateAirport(AirportEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Airports
                        .Single(e => e.AirportId == model.AirportId);

                entity.AirportId = model.AirportId;
                entity.AirportName = model.AirportName;
                entity.AirpotZipCode = model.AirpotZipCode;
                entity.LocationNorth = model.LocationNorth;
                entity.LocationWest = model.LocationWest;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAirport(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Airports
                        .Single(e => e.AirportId == id);

                ctx.Airports.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
