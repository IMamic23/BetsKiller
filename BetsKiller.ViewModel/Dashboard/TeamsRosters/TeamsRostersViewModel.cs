using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.TeamsRosters
{
    public class TeamsRostersViewModel
    {
        public List<string> Teams { get; set; }

        public List<TeamsRostersTeamViewModel> Rosters { get; set; }
    }
}
