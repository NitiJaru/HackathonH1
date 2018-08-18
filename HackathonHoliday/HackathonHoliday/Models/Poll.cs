using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonHoliday.Models
{
    public class Poll
    {
        [BsonId]
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Owner { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public class Choice
    {
        [BsonId]
        public string Id { get; set; }
        public string PollId { get; set; }
        public string Title { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
        public string Owner { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public class Vote
    {
        public double Rating { get; set; }
        public string Owner { get; set; }
        public DateTime CreateAt { get; set; }
    }
}