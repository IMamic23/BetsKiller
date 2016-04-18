using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("RostersNBA")]
    public class RostersNBA
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public DateTime? Birthday { get; set; }

        public int Age { get; set; }

        public string Birthplace { get; set; }

        public decimal HeightIn { get; set; }

        public decimal HeightCm { get; set; }

        public decimal HeightM { get; set; }

        public string HeightFormatted { get; set; }

        public decimal WeightLb { get; set; }

        public decimal WeightKg { get; set; }

        public string Position { get; set; }

        public int UniformNumber { get; set; }

        // Foreign keys
        public int? Team_Id { get; set; }

        // Navigation properties
        [ForeignKey("Team_Id")]
        public virtual TeamsNBA Team { get; set; }
    }
}
