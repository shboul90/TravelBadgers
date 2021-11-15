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
    public class RequestService
    {
        private readonly Guid _userId;

        public RequestService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<SelectListItem> CitiesList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                IEnumerable<SelectListItem> cityList = ctx.Cities.Select(city => new SelectListItem
                {
                    Text = city.CityName,
                    Value = city.CityId.ToString()
                });
                return cityList.ToArray();
            }
        }
        public bool CreateRequest(RequestCreate model)
        {

            var entity =
            new Request()
            {
                OwnerId = _userId,
                CityId = model.CityId,
                OverallBudget = model.OverallBudget,
                DepartDate = model.DepartDate,
                ReturnDate = model.ReturnDate,
                CreatedUtc = DateTimeOffset.UtcNow
            };

            using (var ctx = new ApplicationDbContext())
            {

                ctx.Requests.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<RequestListItem> GetRequests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Requests
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            request =>
                                new RequestListItem
                                {
                                    RequestId = request.RequestId,
                                    CityId = request.CityId,
                                    OverallBudget = request.OverallBudget,
                                    DepartDate = request.DepartDate,
                                    ReturnDate = request.ReturnDate,
                                    CreatedUtc = request.CreatedUtc
                                }
                       );

                return query.ToArray();
            }
        }

        public RequestDetail GetRequestById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Requests
                        .Single(e => e.RequestId == id);
                return
                    new RequestDetail
                    {
                        RequestId = entity.RequestId,
                        CityId = entity.CityId,
                        OverallBudget = entity.OverallBudget,
                        DepartDate = entity.DepartDate,
                        ReturnDate = entity.ReturnDate,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateRequest(RequestEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Requests
                        .Single(e => e.RequestId == model.RequestId && e.OwnerId == _userId );

                entity.RequestId = model.RequestId;
                entity.CityId = model.CityId;
                entity.OverallBudget = model.OverallBudget;
                entity.DepartDate = model.DepartDate;
                entity.ReturnDate = model.ReturnDate;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRequest(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Requests
                        .Single(e => e.RequestId == id);

                ctx.Requests.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
