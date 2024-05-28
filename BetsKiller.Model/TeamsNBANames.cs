using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("TeamsNBANames")]
    public class TeamsNBANames
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameErikberg { get; set; }

        public string NameSportsdatabase { get; set; }

        public string NameRotoworld { get; set; }

        public string NameNBAcom { get; set; }
    }
}
