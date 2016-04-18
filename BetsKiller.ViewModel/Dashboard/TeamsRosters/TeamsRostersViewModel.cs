using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.TeamsRosters
{
    public class TeamsRostersViewModel
    {
        public List<string> Teams { get; set; }

        public List<TeamsRostersTeamViewModel> Rosters { get; set; }
    }
}
