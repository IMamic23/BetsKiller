using System.Collections.Generic;

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
