using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.Injuries
{
    public class InjuriesTeamViewModel
    {
        public string TeamName { get; set; }

        public List<InjuriesPlayerViewModel> Players { get; set; }
    }
}
