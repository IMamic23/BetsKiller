using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.ScheduleResults
{
    public class ScheduleResultsTeamViewModel
    {
        public string TeamName { get; set; }

        public List<ScheduleResultsGameViewModel> Games { get; set; }
    }
}
