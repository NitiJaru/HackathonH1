using HackathonHoliday.Dac;
using HackathonHoliday.Dac.Contract;
using HackathonHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HackathonHoliday.Controllers
{
    public class HomeController : Controller
    {
        private IPollDac pollDac;
        private IChoiceDac choiceDac;

        public HomeController()
        {
            pollDac = new PollDac();
            choiceDac = new ChoiceDac();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username)
        {
            FormsAuthentication.SetAuthCookie(username, true);
            return RedirectToAction(nameof(Polls));
        }

        [HttpGet]
        public async Task<ActionResult> Polls()
        {
            var polls = await pollDac.GetPolls();
            return View(new List<PollInformation>());
        }

        [HttpGet]
        public async Task<ActionResult> CreatePoll()
        {
            return View(new PollInformation());
        }

        [HttpPost]
        public async Task<ActionResult> CreatePoll(PollInformation model)
        {
            await pollDac.CreatePoll(new Poll
            {
                Id = Guid.NewGuid().ToString(),
                Topic = model.Name,
                Owner = User.Identity.Name,
                CreateAt = DateTime.UtcNow
            });
            return RedirectToAction(nameof(PollDetail));
        }

        [HttpGet]
        public ActionResult PollDetail(string pollid)
        {
            return View(new PollInformation { Choices = new List<ChoiceInformation>() });
        }

        [HttpPost]
        public async Task<ActionResult> CreateChoice(string pollid, string choicename)
        {
            await choiceDac.CreateChoice(new Choice
            {
                Id = Guid.NewGuid().ToString(),
                PollId = "test",
            });
            return RedirectToAction(nameof(PollDetail));
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