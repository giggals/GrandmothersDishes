using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;
using GrandmothersDishes.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService
{
    public class FoodService : IFoodService
    {
        public FoodService(IRepository<Dish> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public readonly IRepository<Dish> repository;
        private readonly IMapper mapper;

        public async Task<Dish> CreateDish(CreateDishViewModel dishViewModel)
        {
            if (!Enum.TryParse(dishViewModel.DishType, out DishType dishType))
            {
                throw new Exception("Invalid Enum Type!");
            }

            var dish = this.mapper.Map<Dish>(dishViewModel);

           await  this.repository.AddAsync(dish);
            this.repository.SaveChanges();
            

            return dish;
        }

        public DetailsDishViewModel GetDishDetails(string id)
        {
            var dish = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (dish == null)
            {
                return null;
            }

            var model = this.mapper.Map<DetailsDishViewModel>(dish);

            model.Calories = Math.Round(model.Calories);

            return model;
        }

        public void EditDish(UpdateDeleteViewModel editModel)
        {
            var dish = this.repository.All().FirstOrDefault(x => x.Id == editModel.Id);

            if (dish == null)
            {
                return;
            }

            if (!Enum.TryParse(editModel.DishType, out DishType dishType))
            {
                return;
            }

            dish.Name = editModel.Name;
            dish.Calories = editModel.Calories;
            dish.Description = editModel.Description;
            dish.DishType = dishType;
            dish.ImageUrl = editModel.ImageUrl;
            dish.Price = editModel.Price;

            this.repository.SaveChanges();

        }

        public UpdateDeleteViewModel EditDeleteDishGetModel(string id)
        {
            var dish = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (dish == null)
            {
                return null;
            }

            var viewModel = this.mapper.Map<UpdateDeleteViewModel>(dish);

            return viewModel;

        }

        public void DeleteDish(string id)
        {
            var dish = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (dish == null)
            {
                return;
            }

            this.repository.Delete(dish);
            this.repository.SaveChanges();
        }

       
    }
}
