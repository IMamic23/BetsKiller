using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("AnalysisProfit")]
    public class AnalysisProfit
    {
        [Key]
        public int Id { get; set; }

        public int TotalBets { get; set; }
        
        public int Year { get; set; }

        public int Month { get; set; }

        public int Week { get; set; }

        public DateTime FirstDayInWeek { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public decimal Invested { get; set; }

        public decimal ROI { get; set; }

        public decimal Profit { get; set; }

        // Foreign keys
        public int? Sport_Id { get; set; }

        // Navigation propeties
        [ForeignKey("Sport_Id")]
        public virtual Sport Sport { get; set; }
    }
}
