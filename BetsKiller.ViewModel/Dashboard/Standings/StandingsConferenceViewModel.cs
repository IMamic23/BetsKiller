using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.Standings
{
    public class StandingsConferenceViewModel
    {
        public string ConferenceName { get; set; }

        public string ConferenceLabel { get; set; }

        public string ConferenceStyle { get; set; }

        public List<StandingsTeamViewModel> Standings { get; set; }
    }
}
