using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core.Constants;
using MoviezLand.Core.Models;

namespace MoviezLand.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =$"{Role.Manager},{Role.Admin}")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) 
                return NotFound();
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exception();
            return Ok();
        }
    }
}
