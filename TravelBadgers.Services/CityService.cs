using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;
using TravelBadgers.Models;

namespace TravelBadgers.Services
{
    public class CityService
    {
        private readonly Guid _userId;

        public CityService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCity(CityCreate model)
        {
            var entity =
                new City()
                {
                    OwnerId = _userId,
                    CityName = model.CityName,
                    CityZipCode = model.CityZipCode,
                    LocationNorth = model.LocationNorth,
                    LocationWest = model.LocationWest,
                    AvgHotelDailyCost = model.AvgHotelDailyCost,
                    AvgEntertainmentDaily = model.AvgEntertainmentDaily,
                    AvgFoodDaily = model.AvgFoodDaily,
                    Cuisine = model.Cuisine,
                    Climate = model.Climate,
                    Terrain = model.Terrain,
                    CityRating = model.CityRating
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cities.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CityListItem> GetCities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Cities
                        .Select(
                            city =>
                                new CityListItem
                                {
                                    CityId = city.CityId,
                                    CityName = city.CityName,
                                    AvgHotelDailyCost = city.AvgHotelDailyCost,
                                    AvgEntertainmentDaily = city.AvgEntertainmentDaily,
                                    AvgFoodDaily = city.AvgFoodDaily,
                                    Cuisine = city.Cuisine,
                                    Climate = city.Climate,
                                    Terrain = city.Terrain,
                                    CityRating = city.CityRating
                                }
                       );

                return query.ToArray();
            }
        }

        public CityDetail GetCityById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cities
                        .Single(e => e.CityId == id);
                return
                    new CityDetail
                    {
                        CityId = entity.CityId,
                        CityName = entity.CityName,
                        CityZipCode = entity.CityZipCode,
                        LocationNorth = entity.LocationNorth,
                        LocationWest = entity.LocationWest,
                        AvgHotelDailyCost = entity.AvgHotelDailyCost,
                        AvgEntertainmentDaily = entity.AvgEntertainmentDaily,
                        AvgFoodDaily = entity.AvgFoodDaily,
                        Cuisine = entity.Cuisine,
                        Climate = entity.Climate,
                        Terrain = entity.Terrain,
                        CityRating = entity.CityRating
                    };
            }
        }

        public bool UpdateCity(CityEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cities
                        .Single(e => e.CityId == model.CityId);

                entity.CityId = model.CityId;
                entity.CityName = model.CityName;
                entity.CityZipCode = model.CityZipCode;
                entity.LocationNorth = model.LocationNorth;
                entity.LocationWest = model.LocationWest;
                entity.AvgHotelDailyCost = model.AvgHotelDailyCost;
                entity.AvgEntertainmentDaily = model.AvgEntertainmentDaily;
                entity.AvgFoodDaily = model.AvgFoodDaily;
                entity.Cuisine = model.Cuisine;
                entity.Climate = model.Climate;
                entity.Terrain = model.Terrain;
                entity.CityRating = model.CityRating;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCity(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cities
                        .Single(e => e.CityId == id);

                ctx.Cities.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
