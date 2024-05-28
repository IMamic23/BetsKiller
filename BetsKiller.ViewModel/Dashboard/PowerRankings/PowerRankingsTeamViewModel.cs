using BetsKiller.Helper.HTML;

namespace BetsKiller.ViewModel.Dashboard.PowerRankings
{
    public class PowerRankingsTeamViewModel
    {
        public string TeamName { get; set; }

        public CustomHtmlElement Rank { get; set; }

        public string RankLastWeek { get; set; }

        public string Score { get; set; }

        public string Pace { get; set; }

        public string OffRtg { get; set; }

        public string DefRtg { get; set; }

        public string NetRtg { get; set; }

        public string GamesThisWeek { get; set; }
    }
}
