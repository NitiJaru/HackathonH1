using HackathonHoliday.Dac.Contract;
using HackathonHoliday.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HackathonHoliday.Dac
{
    public class PollDac : IPollDac
    {
        private IMongoCollection<Poll> collection;

        public PollDac()
        {
            var client = new MongoClient("mongodb://unipopcorn:MTu3D1EBGdfcAoFpC4GpzUHzqELbRdSfuLZb01Exsdunf4wKv8y6dT7MbANP4y6w8UqlUtvFAuMAD22YbfxF3g==@unipopcorn.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            var database = client.GetDatabase("unipopcorn");
            collection = database.GetCollection<Poll>("poll");
        }

        public async Task CreatePoll(Poll model)
        {
            await collection.InsertOneAsync(model);
        }

        public async Task<Poll> GetPoll(string id)
        {
            var model = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return model;
        }

        public async Task<IEnumerable<Poll>> GetPolls()
        {
            var model = await collection.Find(x => true).ToListAsync();
            return model;
        }
    }
}