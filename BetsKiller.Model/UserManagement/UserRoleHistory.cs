using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model.UserManagement
{
    [Table("UsersRolesHistory")]
    public class UserRoleHistory
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        public DateTime Changed { get; set; }

        public decimal? MoneyPaid { get; set; }

        public string PaymentDescription { get; set; }

        // Foreign keys
        public int? Old_RoleId { get; set; }
        public int? New_RoleId { get; set; }
        public int? PaymentSource_Id { get; set; }

        // Navigation properties
        [ForeignKey("Old_RoleId")]
        public virtual Role OldRole { get; set; }

        [ForeignKey("New_RoleId")]
        public virtual Role NewRole { get; set; }

        [ForeignKey("PaymentSource_Id")]
        public virtual PaymentSource PaymentSource { get; set; }
    }
}
