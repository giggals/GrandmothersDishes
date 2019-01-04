using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using GrandmothersDishes.Models;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class DrinkProfile : Profile
    {
        public DrinkProfile()
        {
            CreateMap<CreateDrinkViewModel, Drink>();
            CreateMap<Drink, DrinkDetailsViewModel>();
            CreateMap<Drink, DrinkEditDeleteViewModel>();
        }
    }
}
