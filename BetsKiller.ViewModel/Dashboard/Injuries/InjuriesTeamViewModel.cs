using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.Injuries
{
    public class InjuriesTeamViewModel
    {
        public string TeamName { get; set; }

        public List<InjuriesPlayerViewModel> Players { get; set; }
    }
}
