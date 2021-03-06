﻿
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public AccountController(IUsersService usersService,
            SignInManager<GrandMothersUser> signInManager
            )
        {
            this.usersService = usersService;
            this.signInManager = signInManager;
        }

        private readonly IUsersService usersService;
        private readonly SignInManager<GrandMothersUser> signInManager;


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.usersService.MapFromRegisterViewModel(model);
            
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