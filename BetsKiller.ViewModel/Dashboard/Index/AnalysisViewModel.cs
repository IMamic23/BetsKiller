using BetsKiller.Helper.HTML;

namespace BetsKiller.ViewModel.Dashboard.Index
{
    public class AnalysisViewModel
    {
        public string Id { get; set; }

        public string HomeTeamName { get; set; }
        public string HomeTeamAbbreviation { get; set; }

        public string AwayTeamName { get; set; }
        public string AwayTeamAbbreviation { get; set; }

        public string Pick { get; set; }

        public CustomHtmlElement Confidence { get; set; }

        public string Odds { get; set; }

        public string Invested { get; set; }

        public string Result { get; set; }

        public CustomHtmlElement Status { get; set; }

        public CustomHtmlElement Profit { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string BetType { get; set; }
    }
}
