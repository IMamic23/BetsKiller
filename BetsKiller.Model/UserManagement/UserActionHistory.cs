using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model.UserManagement
{
    [Table("UsersActionsHistory")]
    public class UserActionHistory
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        public string Action { get; set; }

        public DateTime DateTime { get; set; }
    }
}