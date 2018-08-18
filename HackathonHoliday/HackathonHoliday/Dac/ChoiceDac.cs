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
    public class ChoiceDac : IChoiceDac
    {
        private IMongoCollection<Choice> collection;

        public ChoiceDac()
        {
            var client = new MongoClient("mongodb://unipopcorn:MTu3D1EBGdfcAoFpC4GpzUHzqELbRdSfuLZb01Exsdunf4wKv8y6dT7MbANP4y6w8UqlUtvFAuMAD22YbfxF3g==@unipopcorn.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            var database = client.GetDatabase("unipopcorn");
            collection = database.GetCollection<Choice>("choice");
        }

        public async Task CreateChoice(Choice model)
        {
            await collection.InsertOneAsync(model);
        }

        public Task DeleteVote(string choiceId, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Choice>> GetChoiceByPollId(string id)
        {
            var model = await collection.Find(x => x.PollId == id).ToListAsync();
            return model;
        }

        public Task UpdateChoice(Choice model)
        {
            throw new NotImplementedException();
        }
    }
}