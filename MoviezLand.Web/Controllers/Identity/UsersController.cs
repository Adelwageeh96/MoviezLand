using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MoviezLand.Core.Constants;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Users;
using System.Collections.Specialized;
using System.Net.Mail;

namespace MoviezLand.Web.Controllers.Identity
{
    [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            Dictionary<string, IList<string>> userRoles = new Dictionary<string, IList<string>>();
            var allUsers = await userManager.Users.ToListAsync();
            foreach (var user in allUsers)
                userRoles[user.Id] = await userManager.GetRolesAsync(user);

            var users = await userManager.Users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles[user.Id]
            }
            ).ToListAsync();
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (await userManager.FindByEmailAsync(model.Email) is not null)
            {
                ModelState.AddModelError("", "Email is exist");
                return View(model);
            }
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDate = model.BirthDate,
                PasswordHash = model.Password,
                PhoneNumber = model.PhoneNumber,
                UserName = new MailAddress(model.Email).User
            };
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }
            await userManager.AddToRoleAsync(user, Role.User);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> EditUser([FromRoute] string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            return View(new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user is null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(model);
            var userWithSameEmil = await userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmil is not null && userWithSameEmil.Id != model.Id)
            {
                ModelState.AddModelError("Email", "Email is already exist");
                return View(model);
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }



    }
}
