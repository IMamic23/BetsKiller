using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("Sport")]
    public class Sport
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
