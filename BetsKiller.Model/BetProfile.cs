using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("BetProfiles")]
    public class BetProfile
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation properties
        public virtual List<Bet> Bets { get; set; }
    }
}
