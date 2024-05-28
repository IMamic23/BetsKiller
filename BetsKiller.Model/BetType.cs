using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("BetType")]
    public class BetType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
