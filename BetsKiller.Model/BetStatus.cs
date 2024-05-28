using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("BetStatus")]
    public class BetStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
