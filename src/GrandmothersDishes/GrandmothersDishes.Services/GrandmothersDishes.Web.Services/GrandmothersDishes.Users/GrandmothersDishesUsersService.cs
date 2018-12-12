using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users
{
    public class GrandmothersDishesUsersService : IGrandmothersDishesUsersService
    {
        public GrandmothersDishesUsersService(IRepository<GrandMothersUser> repository,
            SignInManager<GrandMothersUser> singInManager)
        {
            this.repository = repository;
            this.singInManager = singInManager;
        }

        private readonly IRepository<GrandMothersUser> repository;

        public readonly SignInManager<GrandMothersUser> singInManager;

        public IEnumerable<GrandMothersUser> AllUsers()
        {
            return this.repository.All();
        }
    }
}
