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
    public class AirportController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            AirportService service = CreateAirportService();
            var model = service.GetAirports();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AirportCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            AirportService service = CreateAirportService();

            if (service.CreateAirport(model))
            {
                TempData["SaveResult"] = "Your airport was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Airport could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            AirportService service = CreateAirportService();
            var model = service.GetAirportById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            AirportService service = CreateAirportService();
            var detail = service.GetAirportById(id);
            var model =
                new AirportEdit
                {
                    AirportId = detail.AirportId,
                    AirportName = detail.AirportName,
                    AirpotZipCode = detail.AirpotZipCode,
                    LocationNorth = detail.LocationNorth,
                    LocationWest = detail.LocationWest,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AirportEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AirportId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            AirportService service = CreateAirportService();

            if (service.UpdateAirport(model))
            {
                TempData["SaveResult"] = "Your airport was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your airport could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            AirportService service = CreateAirportService();
            var model = service.GetAirportById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            AirportService service = CreateAirportService();

            service.DeleteAirport(id);

            TempData["SaveResult"] = "Your airport was deleted";

            return RedirectToAction("Index");
        }

        private AirportService CreateAirportService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AirportService(userId);
            return service;
        }
    }
}