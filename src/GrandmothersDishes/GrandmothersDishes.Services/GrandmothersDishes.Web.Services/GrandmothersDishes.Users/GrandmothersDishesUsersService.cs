using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users
{
    public class GrandmothersDishesUsersService : IGrandmothersDishesUsersService
    {
        public GrandmothersDishesUsersService(IRepository<GrandMothersUser> repository,
            SignInManager<GrandMothersUser> singInManager,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        private readonly IRepository<GrandMothersUser> repository;
        private readonly IMapper mapper;
        public readonly SignInManager<GrandMothersUser> singInManager;

        public IEnumerable<GrandMothersUser> AllUsers()
        {
            return this.repository.All();
        }

        
    }
}
