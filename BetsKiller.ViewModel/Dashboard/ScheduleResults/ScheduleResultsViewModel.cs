using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.ScheduleResults
{
    public class ScheduleResultsViewModel
    {
        public List<string> Teams { get; set; }

        public List<ScheduleResultsTeamViewModel> TeamsGames { get; set; }
    }
}
