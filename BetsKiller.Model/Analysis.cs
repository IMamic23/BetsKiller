using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("Analysis")]
    public class Analysis
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Changed { get; set; }

        public string Description { get; set; }

        public decimal? OfferTotal { get; set; }

        public decimal? OfferLine { get; set; }

        public decimal Bet { get; set; }

        public string BetLogicPush { get; set; }

        public string BetLogicWinLoss { get; set; }

        public decimal Invested { get; set; }

        public decimal Odd { get; set; }

        public decimal? Profit { get; set; }

        public string Result { get; set; }

        public decimal Confidence { get; set; }

        // Foreign keys
        public int? Sport_Id { get; set; }
        public int? BetType_Id { get; set; }
        public int? BetStatus_Id { get; set; }
        public int? AnalyseType_Id { get; set; }
        public int? EventNBA_Id { get; set; }
        public int? EventMLB_Id { get; set; }

        // Navigation properties
        [ForeignKey("Sport_Id")]
        public virtual Sport Sport { get; set; }

        [ForeignKey("BetType_Id")]
        public virtual BetType BetType { get; set; }

        [ForeignKey("BetStatus_Id")]
        public virtual BetStatus BetStatus { get; set; }

        [ForeignKey("AnalyseType_Id")]
        public virtual AnalyseType AnalyseType { get; set; }

        [ForeignKey("EventNBA_Id")]
        public virtual ScheduleResultsNBA EventNBA { get; set; }

        //TODO
        //[ForeignKey("EventMLB_Id")]
        //public virtual ScheduleResultsMLB EventMLB { get; set; }
    }
}
