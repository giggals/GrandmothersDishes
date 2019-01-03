using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;

using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;


namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users
{
    public class UsersService : IUsersService
    {
        public UsersService(IRepository<GrandMothersUser> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        private readonly IRepository<GrandMothersUser> repository;
        private readonly IMapper mapper;

        public IEnumerable<GrandMothersUser> AllUsers()
        {
            return this.repository.All();
        }

        public GrandMothersUser MapFromRegisterViewModel(RegisterViewModel model)
        {
            var user = this.mapper.Map<GrandMothersUser>(model);

            return user;
        }

    }
}
