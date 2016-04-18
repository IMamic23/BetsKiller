using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("ScheduleResultsNBA")]
    public class ScheduleResultsNBA
    {
        [Key]
        public int Id { get; set; }

        public string EventId { get; set; }

        public string EventStatus { get; set; }

        public DateTime? EventStartDateTime { get; set; }

        public string EventSeasonType { get; set; }

        public int TeamEventNumberInSeason { get; set; }

        public string TeamEventLocationType { get; set; }

        public string TeamEventResult { get; set; }

        public int TeamPointsScored { get; set; }

        public string TeamPeriodScores { get; set; }

        public int TeamEventsWon { get; set; }

        public int TeamEventsLost { get; set; }

        public int OpponentPointsScored { get; set; }

        public string OpponentPeriodScores { get; set; }

        public int OpponentEventsWon { get; set; }

        public int OpponentEventsLost { get; set; }

        public int SiteCapacity { get; set; }

        public string SiteSurface { get; set; }

        public string SiteName { get; set; }

        public string SiteState { get; set; }

        public string SiteCity { get; set; }

        // Foreign keys
        public int? Team_Id { get; set; }
        public int? Opponent_Id { get; set; }

        // Navigation properties
        [ForeignKey("Team_Id")]
        public virtual TeamsNBA Team { get; set; }

        [ForeignKey("Opponent_Id")]
        public virtual TeamsNBA Opponent { get; set; }
    }
}
