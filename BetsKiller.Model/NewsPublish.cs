using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model
{
    [Table("NewsPublish")]
    public class NewsPublish
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public DateTime Published { get; set; }

        // Foreign keys
        public int? Sport_Id { get; set; }

        // Navigation properties
        [ForeignKey("Sport_Id")]
        public virtual Sport Sport { get; set; }
    }
}
