using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService
{
    public class FoodService : IFoodService
    {
        public FoodService(IRepository<Dish> dishRepository,
            IMapper mapper)
        {
            this.dishRepository = dishRepository;
            this.mapper = mapper;
        }

        private readonly IRepository<Dish> dishRepository;
        private readonly IMapper mapper;
        
        public Dish CreateDish(CreateDishViewModel dishViewModel)
        {
            if (!Enum.TryParse(dishViewModel.DishType , out DishType dishType))
            {
                throw new Exception("Invalid Enum Type!");
            }

            var dish = this.mapper.Map<Dish>(dishViewModel);

            this.dishRepository.AddAsync(dish);
            this.dishRepository.SaveChangesAsync();
            
            return dish;
        }

      
    }
}
