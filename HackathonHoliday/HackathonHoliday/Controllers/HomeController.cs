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
            var model = new List<PollInformation>();
            foreach (var poll in polls)
            {
                var choices = await choiceDac.GetChoiceByPollId(poll.Id);
                var ch = choices.Select(it => new ChoiceInformation
                {
                    PollRefId = poll.Id,
                    Name = it.Title
                });

                var pollInfo = new PollInformation
                {
                    Id = poll.Id,
                    Name = poll.Topic,
                    OwnerName = poll.Owner,
                    CreateDate = poll.CreateAt,
                    Choices = ch
                };

                model.Add(pollInfo);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> CreatePoll()
        {
            return View(new PollInformation());
        }

        [HttpPost]
        public async Task<ActionResult> CreatePoll(PollInformation model)
        {
            var id = Guid.NewGuid().ToString();
            await pollDac.CreatePoll(new Poll
            {
                Id = id,
                Topic = model.Name,
                Owner = User.Identity.Name,
                CreateAt = DateTime.UtcNow
            });
            return RedirectToAction(nameof(PollDetail), new { pollid = id });
        }

        [HttpGet]
        public async Task<ActionResult> PollDetail(string pollid)
        {
            var poll = await pollDac.GetPoll(pollid);
            var choices = await choiceDac.GetChoiceByPollId(poll.Id);
            var ch = choices.Select(it => {
                var rate = it.Votes != null && it.Votes.Any() ? (int)Math.Floor(it.Votes.Sum(v => v.Rating) / it.Votes.Count()) : 0;
                return new ChoiceInformation
                {
                    PollRefId = poll.Id,
                    Name = it.Title,
                    Rate = rate,
                    Id = it.Id
                };
            });

            var pollInfo = new PollInformation
            {
                Id = poll.Id,
                Name = poll.Topic,
                OwnerName = poll.Owner,
                CreateDate = poll.CreateAt,
                Choices = ch
            };
            return View(pollInfo);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChoice(string id, string choicename)
        {
            if (id == null || choicename == null) return RedirectToAction(nameof(CreatePoll));
            await choiceDac.CreateChoice(new Choice
            {
                Id = Guid.NewGuid().ToString(),
                PollId = id,
                Title = choicename
            });
            return RedirectToAction(nameof(PollDetail), new { pollid = id });
        }

        [HttpGet]
        public async Task<ActionResult> ChoiceDetail(string choiceid)
        {
            var choice = await choiceDac.GetChoice(choiceid);
            var poll = await pollDac.GetPoll(choice.PollId);
            var rate = choice.Votes != null && choice.Votes.Any() ? (int)Math.Floor(choice.Votes.Sum(it => it.Rating) / choice.Votes.Count()) : 0 ;
            var model = new DisplayChoiceInformation
            {
                Id = choice.Id,
                Name = choice.Title,
                PollRefId = poll.Id,
                PollCreateDate = poll.CreateAt,
                PollName = poll.Topic,
                PollOwnerName = poll.Owner,
                Rate = rate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChoiceDetail(DisplayChoiceInformation model)
        {
            var choice = await choiceDac.GetChoice(model.Id);
            if (choice.Votes == null) choice.Votes = new List<Vote>();

            var voted = choice.Votes.FirstOrDefault(it => it.Owner == User.Identity.Name);
            var votes = choice.Votes.ToList();
            if (voted != null)
            {
                var voteIndex = votes.IndexOf(voted);
                votes[voteIndex].Rating = model.Rate;
                votes[voteIndex].CreateAt = DateTime.UtcNow;
               
            }
            else
            {
                votes.Add(new Vote
                {
                    Owner = User.Identity.Name,
                    Rating = model.Rate,
                    CreateAt = DateTime.UtcNow
                });
            }
            choice.Votes = votes;
            await choiceDac.UpdateChoice(choice);
            return RedirectToAction(nameof(PollDetail), new { pollid = model.PollRefId });
        }
    }
}