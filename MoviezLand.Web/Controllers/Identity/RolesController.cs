using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviezLand.Core.Constants;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Roles;
using MoviezLand.Web.ViewModels.Users;

namespace MoviezLand.Web.Controllers.Identity
{
    [Authorize(Roles = Role.Manager)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index() => View(await roleManager.Roles.ToListAsync());

        public async Task<IActionResult> ManageRoleUsers([FromRoute] string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role is null)
                return NotFound();
            var users = await userManager.Users.ToListAsync();
            var viewModel = new RoleUsersViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Users = users.Select(user => new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsInTheRole = userManager.IsInRoleAsync(user, role.Name).Result
                }
                ).ToList()
            };

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoleUsers(RoleUsersViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role is null)
                return NotFound();
            var usersInRole = await userManager.GetUsersInRoleAsync(model.RoleName);
            foreach (var user in model.Users)
            {
                bool userInRole = usersInRole.Any(u => u.Id == user.UserId);
                ApplicationUser userToModify = userManager.Users.Where(u => u.Id == user.UserId).FirstOrDefault();
                if (userInRole && !user.IsInTheRole)
                {
                    await userManager.RemoveFromRoleAsync(userToModify, model.RoleName);
                }
                else if (!userInRole && user.IsInTheRole)
                {
                    await userManager.AddToRoleAsync(userToModify, model.RoleName);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
