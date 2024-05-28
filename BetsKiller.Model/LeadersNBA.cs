using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("LeadersNBA")]
    public class LeadersNBA
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public int Rank { get; set; }

        public decimal Value { get; set; }

        // Foreign keys
        public int? Category_Id { get; set; }
        public int? Team_Id { get; set; }

        // Navigation properties
        [ForeignKey("Category_Id")]
        public virtual LeadersNBACategories Category { get; set; }

        [ForeignKey("Team_Id")]
        public virtual TeamsNBA Team { get; set; }
    }
}
