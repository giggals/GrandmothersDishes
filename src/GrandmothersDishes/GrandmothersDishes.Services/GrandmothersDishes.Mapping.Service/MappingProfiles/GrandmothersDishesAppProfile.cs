using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;
using GrandmothersDishes.Web.ViewModels.Account;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class GrandmothersDishesAppProfile : Profile
    {
        public GrandmothersDishesAppProfile()
        {
            CreateMap<RegisterViewModel, GrandMothersUser>();
            CreateMap<CreateDishViewModel, Dish>();
            CreateMap<Dish, DetailsDishViewModel>();
            CreateMap<Dish, UpdateDeleteViewModel>();
            CreateMap<CreateVehicleViewModel, Vehicle>();
            CreateMap<CreateEmployeeViewModel, Employee>();
            CreateMap<CreateDrinkViewModel, Drink>();
            CreateMap<Drink, DrinkDetailsViewModel>();
            CreateMap<DeliverViewModel, Delivery>();
            CreateMap<CreateCardViewModel, DiscountCard>();
        }

    }
}
