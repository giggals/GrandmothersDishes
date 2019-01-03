
using GrandmothersDishes.Models;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts
{
    public interface IUsersService
    {
        IEnumerable<GrandMothersUser> AllUsers();

        GrandMothersUser MapFromRegisterViewModel(RegisterViewModel model);
    }
}
