using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBadgers.Data;
using TravelBadgers.Models;
using TravelBadgers.Services;

namespace TravelBadgers.WebMVC.Controllers
{
    public class TripController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var service = CreateTripService();
            var model = service.GetTrips();

            return View(model);
        }
        public ActionResult Create()
        {
            var service = CreateTripService();
            var viewModel = new TripCreate();

            viewModel.Request = service.RequestsList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Request model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTripService();

            var modeToUse = service.GetRequestById(model.RequestId);

            if (service.CreateTrip(modeToUse))
            {
                TempData["SaveResult"] = "Your Trip was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Trip could not be created.");
            var viewModel = new TripCreate();

            viewModel.Request = service.RequestsList();

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var service = CreateTripService();
            var model = service.GetTripById(id);

            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var service = CreateTripService();
            var model = service.GetTripById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTripService();

            service.DeleteTrip(id);

            TempData["SaveResult"] = "Your trip was deleted";

            return RedirectToAction("Index");
        }

        private TripService CreateTripService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TripService(userId);
            return service;
        }
    }
}