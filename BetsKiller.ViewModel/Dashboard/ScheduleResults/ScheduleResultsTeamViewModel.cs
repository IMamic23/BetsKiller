using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.ScheduleResults
{
    public class ScheduleResultsTeamViewModel
    {
        public string TeamName { get; set; }

        public List<ScheduleResultsGameViewModel> Games { get; set; }
    }
}
