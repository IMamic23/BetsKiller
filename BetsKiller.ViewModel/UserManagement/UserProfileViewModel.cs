namespace BetsKiller.ViewModel.UserManagement
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string FullName { get; set; }
        public string RoleActiveFrom { get; set; }
        public string RoleActiveTo { get; set; }
    }
}
