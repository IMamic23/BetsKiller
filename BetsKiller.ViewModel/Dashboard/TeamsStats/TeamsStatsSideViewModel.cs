using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.TeamsStats
{
    public class TeamsStatsSideViewModel
    {
        public string SideName { get; set; }

        public string SideLabel { get; set; }

        public List<TeamStatViewModel> Stats { get; set; }
    }
}
