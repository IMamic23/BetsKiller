using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("Bets")]
    public class Bet
    {
        [Key]
        public int Id { get; set; }

        public decimal Invested { get; set; }

        public decimal Odd { get; set; }

        public decimal? Profit { get; set; }

        // Foreign keys
        public int? BetStatus_Id { get; set; }

        // Navigation properties
        [ForeignKey("BetStatus_Id")]
        public virtual BetStatus BetStatus { get; set; }

        public virtual List<BetProfile> BetProfiles { get; set; }

        public virtual List<BetGame> BetGames { get; set; }
    }
}
