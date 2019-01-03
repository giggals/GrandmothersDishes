using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Models;
using GrandmothersDishes.Web.ViewModels.Account;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterViewModel, GrandMothersUser>();
        }
    }
}
