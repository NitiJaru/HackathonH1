using HackathonHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonHoliday.Dac.Contract
{
    public interface IChoiceDac
    {
        Task CreateChoice(Choice model);
        Task UpdateChoice(Choice model);
        Task DeleteVote(string choiceId, string username);
        Task<IEnumerable<Choice>> GetChoiceByPollId(string id);
        Task<Choice> GetChoice(string id);
    }
}
