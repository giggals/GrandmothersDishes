using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishViewModel, Dish>();
            CreateMap<Dish, DetailsDishViewModel>();
        }
    }
}
