using HackathonHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonHoliday.Dac.Contract
{
    public interface IPollDac
    {
        Task CreatePoll(Poll model);
        Task<IEnumerable<Poll>> GetPolls();
    }
}
