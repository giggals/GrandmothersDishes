using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class AccountController : Controller
    {
        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{
        //    await signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        public IActionResult Register()
        {
            return this.View();
        }

        //[HttpPost]
        //public IActionResult Register(RegisterViewModel model)
        //{
        //    //With AutoMapper
        //    var user = mapper.Map<EventuresUser>(model);

        //    //Without AutoMapper 

        //    //EventuresUser user = new EventuresUser()
        //    //{
        //    //    Email = model.Email,
        //    //    UserName = model.Username
        //    //};

        //    IdentityResult result = this.signIn.UserManager.CreateAsync(user, model.Password).Result;

        //    if (result.Succeeded)
        //    {
        //        return this.RedirectToAction("Login", "Account");
        //    }

        //    return this.View();
        //}
    }
}