using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("TeamsNBA")]
    public class TeamsNBA
    {
        [Key]
        public int Id { get; set; }

        public string Abbreviation { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Conference { get; set; }

        public string Division { get; set; }

        public string SiteName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        #region Stats

        public string GamesInSeason { get; set; }

        public string BothSU { get; set; }
        public string BothATS { get; set; }
        public string BothOU { get; set; }
        public string BothAvgLine { get; set; }
        public string BothAvgTotal { get; set; }
        public string BothAvgPoints { get; set; }
        public string BothAvgAssists { get; set; }
        public string BothAvgRebounds { get; set; }
        public string BothAvgBlocks { get; set; }
        public string BothAvgFouls { get; set; }
        public string BothAvgTurnovers { get; set; }

        public string HomeSU { get; set; }
        public string HomeATS { get; set; }
        public string HomeOU { get; set; }
        public string HomeAvgLine { get; set; }
        public string HomeAvgTotal { get; set; }
        public string HomeAvgPoints { get; set; }
        public string HomeAvgAssists { get; set; }
        public string HomeAvgRebounds { get; set; }
        public string HomeAvgBlocks { get; set; }
        public string HomeAvgFouls { get; set; }
        public string HomeAvgTurnovers { get; set; }

        public string AwaySU { get; set; }
        public string AwayATS { get; set; }
        public string AwayOU { get; set; }
        public string AwayAvgLine { get; set; }
        public string AwayAvgTotal { get; set; }
        public string AwayAvgPoints { get; set; }
        public string AwayAvgAssists { get; set; }
        public string AwayAvgRebounds { get; set; }
        public string AwayAvgBlocks { get; set; }
        public string AwayAvgFouls { get; set; }
        public string AwayAvgTurnovers { get; set; }

        #endregion

        // Foreign keys
        public int? Name_Id { get; set; }

        // Navigation properties
        [ForeignKey("Name_Id")]
        public virtual TeamsNBANames Name { get; set; }
    }
}
