using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBadgers.Models;
using TravelBadgers.Services;

namespace TravelBadgers.WebMVC.Controllers
{
    public class RequestController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var service = CreateRequestService();
            var model = service.GetRequests();

            return View(model);
        }

        public ActionResult Create()
        {
            var service = CreateRequestService();
            var viewModel = new RequestCreate();

            viewModel.City = service.CitiesList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRequestService();
            if(model.DepartDate <= DateTime.Now)
            {
                TempData["DepartDate"] = "Your Depart Date most be at least one day out";
                return RedirectToAction("Create");
            }
            else if (model.ReturnDate != null && model.ReturnDate <= model.DepartDate)
            {
                TempData["ArrivalDate"] = "Your Return Date most be greater than your Depart Date.";
                return RedirectToAction("Create");
            }
            else if (service.CreateRequest(model))
            {
                TempData["SaveResult"] = "Your request was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Request could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateRequestService();
            var model = service.GetRequestById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRequestService();
            var detail = service.GetRequestById(id);
            var model =
                new RequestEdit
                {
                    RequestId = detail.RequestId,
                    CityId = detail.CityId,
                    City = service.CitiesList(),
                    OverallBudget = detail.OverallBudget,
                    DepartDate = detail.DepartDate,
                    ReturnDate = detail.ReturnDate,
                    ModifiedUtc = DateTimeOffset.UtcNow
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RequestEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RequestId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRequestService();

            if (service.UpdateRequest(model))
            {
                TempData["SaveResult"] = "Your request was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your request could not be updated.");
            return View();
        }


        public ActionResult Delete(int id)
        {
            var service = CreateRequestService();
            var model = service.GetRequestById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRequestService();

            service.DeleteRequest(id);

            TempData["SaveResult"] = "Your request was deleted";

            return RedirectToAction("Index");
        }

        private RequestService CreateRequestService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RequestService(userId);
            return service;
        }

    }
}