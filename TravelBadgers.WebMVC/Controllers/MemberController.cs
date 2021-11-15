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
    public class MemberController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            MemberService service = CreateMemberService();
            var model = service.GetMemberList();
            
            return View(model);
        }

        public ActionResult Create()
        {
            MemberService service = CreateMemberService();
            MemberCreate viewModel = new MemberCreate();
            
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            MemberService service = CreateMemberService();

            if (service.CreateMember(model))
            {
                TempData["SaveResult"] = "Your data was saved.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Member could not be created.");

            return View(model);
        }

        public ActionResult Details()
        {
            MemberService service = CreateMemberService();
            MemberDetail model = service.GetMember();

            return View(model);
        }

        public ActionResult Edit()
        {
            MemberService service = CreateMemberService();
            MemberDetail detail = service.GetMember();
            var model =
                new MemberEdit
                {
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    StreetAddress = detail.StreetAddress,
                    City = detail.City,
                    State = detail.State,
                    ZipCode = detail.ZipCode,
                    PhoneNumber = detail.PhoneNumber
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            MemberService service = CreateMemberService();

            if (service.UpdateMember(model))
            {
                TempData["SaveResult"] = "Your member information was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your member information could not be updated.");
            return View();
        }

        private MemberService CreateMemberService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MemberService(userId);
            return service;
        }

    }
}