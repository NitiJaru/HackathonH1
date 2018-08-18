using HackathonHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonHoliday.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username)
        {
            return RedirectToAction(nameof(Polls));
        }
        
        [HttpGet]
        public ActionResult Polls()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreatePoll()
        {
            return View(new PollInformation());
        }

        [HttpPost]
        public ActionResult CreatePoll(PollInformation model)
        {
            return RedirectToAction(nameof(PollDetail));
        }

        [HttpGet]
        public ActionResult PollDetail()
        {
            return View(new PollInformation());
        }

        public ActionResult ChoiceDetail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChoiceDetail(ChoiceInformation model)
        {
            return RedirectToAction(nameof(PollDetail));
        }
    }
}