using System.ComponentModel.DataAnnotations;

namespace MoviezLand.Web.ViewModels.Roles
{
    public class RolesViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
