using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.TeamsRosters
{
    public class TeamsRostersTeamViewModel
    {
        public string TeamName { get; set; }

        public List<TeamsRostersPlayerViewModel> Players { get; set; }
    }
}
