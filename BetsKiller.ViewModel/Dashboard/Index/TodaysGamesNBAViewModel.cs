namespace BetsKiller.ViewModel.Dashboard.Index
{
    public class TodaysGamesNBAViewModel
    {
        public string EventId { get; set; }

        public string EventStart { get; set; }

        public string EventStatus { get; set; }

        public string SeasonType { get; set; }

        public string SiteCapacity { get; set; }

        public string SiteSurface { get; set; }

        public string SiteName { get; set; }

        public string SiteState { get; set; }

        public string SiteCity { get; set; }

        public string HomeTeamName { get; set; }
        public string HomeTeamAbbreviation { get; set; }
        public string HomeTeamSU { get; set; }
        public string HomeTeamATS { get; set; }
        public string HomeTeamOU { get; set; }        

        public string AwayTeamName { get; set; }
        public string AwayTeamAbbreviation { get; set; }
        public string AwayTeamSU { get; set; }
        public string AwayTeamATS { get; set; }
        public string AwayTeamOU { get; set; }

        public string HomePointsScored { get; set; }
        public string HomePeriodScores { get; set; }

        public string AwayPointsScored { get; set; }
        public string AwayPeriodScores { get; set; }
    }
}
