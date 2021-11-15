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
    public class CityController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            CityService service = CreateCityService();
            var model = service.GetCities();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            CityService service = CreateCityService();

            if (service.CreateCity(model))
            {
                TempData["SaveResult"] = "Your city was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "City could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            CityService service = CreateCityService();
            var model = service.GetCityById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            CityService service = CreateCityService();
            var detail = service.GetCityById(id);
            var model =
                new CityEdit
                {
                    CityId = detail.CityId,
                    CityName = detail.CityName,
                    CityZipCode = detail.CityZipCode,
                    LocationNorth = detail.LocationNorth,
                    LocationWest = detail.LocationWest,
                    AvgHotelDailyCost = detail.AvgHotelDailyCost,
                    AvgEntertainmentDaily = detail.AvgEntertainmentDaily,
                    AvgFoodDaily = detail.AvgFoodDaily,
                    Cuisine = detail.Cuisine,
                    Climate = detail.Climate,
                    Terrain = detail.Terrain,
                    CityRating = detail.CityRating
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CityId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            CityService service = CreateCityService();

            if (service.UpdateCity(model))
            {
                TempData["SaveResult"] = "Your city was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your city could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            CityService service = CreateCityService();
            var model = service.GetCityById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            CityService service = CreateCityService();

            service.DeleteCity(id);

            TempData["SaveResult"] = "Your city was deleted";

            return RedirectToAction("Index");
        }

        private CityService CreateCityService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CityService(userId);
            return service;
        }
    }
}