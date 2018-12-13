
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Data;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public AccountController(IGrandmothersDishesUsersService usersService,
            SignInManager<GrandMothersUser> signInManager)
        {
            this.usersService = usersService;
            this.signInManager = signInManager;
        }

        private readonly IGrandmothersDishesUsersService usersService;

        private readonly SignInManager<GrandMothersUser> signInManager;
      

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //With AutoMapper
            //var user = mapper.Map<EventuresUser>(model);

            //Without AutoMapper 

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            
            GrandMothersUser user = new GrandMothersUser()
            {
                UserName = model.Username,
                City = model.City,
                HomeAddress = model.HomeAddress,
                Email = model.Email,

            };


            IdentityResult result = this.signInManager.UserManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            else
            {
                var usernameExist = this.usersService.AllUsers().FirstOrDefault(x => x.UserName == model.Username);
                
                foreach (var error in result.Errors)
                {
                    if (usernameExist != null)
                    {
                        ModelState.AddModelError("Username", error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error accured while trying to register!");
                    }
                }
            }

            return this.View(model);
        }
    }
}