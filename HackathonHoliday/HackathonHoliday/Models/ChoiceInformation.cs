using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonHoliday.Models
{
    public class ChoiceInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public string PollRefId { get; set; }
    }

    public class DisplayChoiceInformation : ChoiceInformation
    {
        public string PollName { get; set; }
        public DateTime PollCreateDate { get; set; }
        public string PollOwnerName { get; set; }
    }
}