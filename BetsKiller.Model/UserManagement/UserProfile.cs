using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model.UserManagement
{
    [Table("UserProfile")]
    public class UserProfile
    {
        #region Properties

        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public DateTime RoleActiveFrom { get; set; }

        public DateTime? RoleActiveTo { get; set; }

        // Navigation properties
        public virtual ICollection<Role> Roles { get; set; }

        #endregion

        #region Constructor

        public UserProfile()
        {
            this.Roles = new List<Role>();
        }

        #endregion
    }
}
