using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Model.UserManagement
{
    [Table("webpages_Roles")]
    public class Role
    {
        #region Properties

        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        // Navigation properties
        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        #endregion

        #region Constructor

        public Role()
        {
            this.UserProfiles = new List<UserProfile>();
        }

        #endregion
    }
}
