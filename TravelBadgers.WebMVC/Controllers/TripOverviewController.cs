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
    public class TripOverviewController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var service = CreateTripOverviewService();
            var model = service.GetTripOverviews();

            return View(model);
        }
        public ActionResult Create()
        {
            var service = CreateTripOverviewService();
            var viewModel = new TripOverviewCreate();
            
            viewModel.Request = service.RequestsList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Request model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTripOverviewService();

            var modeToUse = service.GetRequestById(model.RequestId);

            if (service.CreateTripOverview(modeToUse))
            {
                TempData["SaveResult"] = "Your Trip was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Trip could not be created.");

            return View(model);
        }

        private TripOverviewService CreateTripOverviewService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TripOverviewService(userId);
            return service;
        }
    }
}