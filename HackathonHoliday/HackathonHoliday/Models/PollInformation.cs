using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonHoliday.Models
{
    public class PollInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ChoiceInformation> Choices { get; set; }
    }
}