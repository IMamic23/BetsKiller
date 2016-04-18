using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("PowerRankingsNBA")]
    public class PowerRankingsNBA
    {
        [Key]
        public int Id { get; set; }

        public int? Rank { get; set; }

        public int? RankLastWeek { get; set; }

        public string Score { get; set; }

        public string Pace { get; set; }

        public string OffRtg { get; set; }

        public string DefRtg { get; set; }

        public string NetRtg { get; set; }

        public string Description { get; set; }

        public string GamesThisWeek { get; set; }

        // Foreign keys
        public int? Team_Id { get; set; }

        // Navigation properties
        [ForeignKey("Team_Id")]
        public virtual TeamsNBA Team { get; set; }
    }
}
