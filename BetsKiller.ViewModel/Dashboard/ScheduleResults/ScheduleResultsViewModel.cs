using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.ScheduleResults
{
    public class ScheduleResultsViewModel
    {
        public List<string> Teams { get; set; }

        public List<ScheduleResultsTeamViewModel> TeamsGames { get; set; }
    }
}
