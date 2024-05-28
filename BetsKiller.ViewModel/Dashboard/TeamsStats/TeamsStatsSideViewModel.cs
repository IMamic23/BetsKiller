using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.TeamsStats
{
    public class TeamsStatsSideViewModel
    {
        public string SideName { get; set; }

        public string SideLabel { get; set; }

        public List<TeamStatViewModel> Stats { get; set; }
    }
}
