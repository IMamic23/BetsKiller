using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("LeadersNBACategories")]
    public class LeadersNBACategories
    {
        [Key]
        public int Id { get; set; }

        public string NameErikberg { get; set; }

        public string Name { get; set; }
    }
}
