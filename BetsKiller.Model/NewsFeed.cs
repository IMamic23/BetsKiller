﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model
{
    [Table("NewsFeed")]
    public class NewsFeed
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        // Foreign keys
        public int? Sport_Id { get; set; }

        // Navigation properties
        [ForeignKey("Sport_Id")]
        public virtual Sport Sport { get; set; }
    }
}
