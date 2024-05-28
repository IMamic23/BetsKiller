using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetsKiller.Model.UserManagement
{
    [Table("webpages_OAuthMembership")]
    public class OAuthMembership
    {
        [Key, Column(Order = 0)]
        public string Provider { get; set; }

        [Key, Column(Order = 1)]
        public string ProviderUserId { get; set; }

        // Foreign key with "UserProfile" table
        public int UserId { get; set; }
    }
}
