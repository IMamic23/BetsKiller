using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.TeamsRosters
{
    public class TeamsRostersTeamViewModel
    {
        public string TeamName { get; set; }

        public List<TeamsRostersPlayerViewModel> Players { get; set; }
    }
}
