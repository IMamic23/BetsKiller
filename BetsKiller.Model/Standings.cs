using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("Standings")]
    public class Standings
    {
        [Key]
        public int Id { get; set; }

        public int Rank { get; set; }

        public string OrdinalRank { get; set; }

        public int Won { get; set; }

        public int Lost { get; set; }

        public int AwayWon { get; set; }

        public int AwayLost { get; set; }

        public string Conference { get; set; }

        public int ConferenceWon { get; set; }

        public int ConferenceLost { get; set; }

        public string Division { get; set; }

        public decimal GamesBack { get; set; }

        public int GamesPlayed { get; set; }

        public int HomeWon { get; set; }

        public int HomeLost { get; set; }

        public string LastFive { get; set; }

        public string LastTen { get; set; }

        public int PlayoffSeed { get; set; }

        public int PointDifferential { get; set; }

        public string PointDifferentialPerGame { get; set; }

        public int PointsAgainst { get; set; }

        public int PointsFor { get; set; }

        public string PointsAllowedPerGame { get; set; }

        public string PointsScoredPerGame { get; set; }

        public int StreakTotal { get; set; }

        public string Streak { get; set; }

        public string StreakType { get; set; }

        public string WinPercentage { get; set; }

        // Foreign keys
        public int? Sport_Id { get; set; }
        public int? TeamNBA_Id { get; set; }
        public int? TeamMLB_Id { get; set; }

        // Navigation properties
        [ForeignKey("Sport_Id")]
        public virtual Sport Sport { get; set; }

        [ForeignKey("TeamNBA_Id")]
        public virtual TeamsNBA TeamNBA { get; set; }

        //TODO
        //[ForeignKey("TeamMLB_Id")]
        //public virtual ScheduleResultsMLB TeamMLB { get; set; }
    }
}
