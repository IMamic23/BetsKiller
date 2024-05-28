using BetsKiller.Helper.HTML;

namespace BetsKiller.ViewModel.Dashboard.SportsPicks
{
    public class SportsPicksAnalyseViewModel
    {
        public string Date { get; set; }

        public string HomeTeamAbbreviation { get; set; }

        public string AwayTeamAbbreviation { get; set; }

        public string Pick { get; set; }

        public string Result { get; set; }

        public string Invested { get; set; }

        public CustomHtmlElement Status { get; set; }

        public CustomHtmlElement Profit { get; set; }
    }
}
