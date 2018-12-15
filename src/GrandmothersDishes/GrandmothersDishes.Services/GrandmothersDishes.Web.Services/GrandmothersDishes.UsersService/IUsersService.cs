
using GrandmothersDishes.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts
{
    public interface IUsersService
    {
        IEnumerable<GrandMothersUser> AllUsers();
    }
}
