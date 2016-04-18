using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.Standings
{
    public class StandingsTeamViewModel
    {
        public string TeamName { get; set; }

        public string Rank { get; set; }

        public string Won { get; set; }

        public string Lost { get; set; }

        public string AwayWon { get; set; }

        public string AwayLost { get; set; }

        public string ConferenceWon { get; set; }

        public string ConferenceLost { get; set; }

        public string Division { get; set; }

        public string GamesBack { get; set; }

        public string GamesPlayed { get; set; }

        public string HomeWon { get; set; }

        public string HomeLost { get; set; }

        public string LastFive { get; set; }

        public string LastTen { get; set; }

        public string PlayoffSeed { get; set; }

        public string PointDifferential { get; set; }

        public string PointDifferentialPerGame { get; set; }

        public string PointsAgainst { get; set; }

        public string PointsFor { get; set; }

        public string PointsAllowedPerGame { get; set; }

        public string PointsScoredPerGame { get; set; }

        public string Streak { get; set; }

        public string WinPercentage { get; set; }
    }
}
