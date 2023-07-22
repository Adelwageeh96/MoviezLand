using MoviezLand.Web.ViewModels.Users;

namespace MoviezLand.Web.ViewModels.Roles
{
    public class RoleUsersViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}
