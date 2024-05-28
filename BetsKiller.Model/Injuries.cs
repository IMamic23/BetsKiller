using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("Injuries")]
    public class Injuries
    {
        [Key]
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public string PlayerPosition { get; set; }

        public string PlayerStatus { get; set; }

        public string Date { get; set; }

        public string Type { get; set; }

        public string Returns { get; set; }

        public string Report { get; set; }

        public string ReportUpdateDate { get; set; }

        // Foreign keys
        public int? Sport_Id { get; set; }
        public int? TeamNBA_Id { get; set; }

        // Navigation properties
        [ForeignKey("Sport_Id")]
        public virtual Sport Sport { get; set; }

        [ForeignKey("TeamNBA_Id")]
        public virtual TeamsNBA TeamNBA { get; set; }
    }
}
