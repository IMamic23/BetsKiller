using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("DraftsNBA")]
    public class DraftsNBA
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

        public int Round { get; set; }

        public int Pick { get; set; }

        public string OrdinalPick { get; set; }

        public int OverallPick { get; set; }

        public string OrdinalOverallPick { get; set; }

        public int GamesPlayed { get; set; }

        public int Points { get; set; }

        public int Assists { get; set; }

        public int DefensiveRebounds { get; set; }

        public int OffensiveRebounds { get; set; }

        public int Steals { get; set; }

        public int Blocks { get; set; }

        // Foreign keys
        public int? Team_Id { get; set; }

        // Navigation properties
        [ForeignKey("Team_Id")]
        public virtual TeamsNBA Team { get; set; }
    }
}
