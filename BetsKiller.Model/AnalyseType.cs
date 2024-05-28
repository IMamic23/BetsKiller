using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("AnalyseType")]
    public class AnalyseType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
