using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService
{
    public class DrinksService : IDrinkService
    {
        public DrinksService(IRepository<Drink> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        private readonly IRepository<Drink> repository;
        private readonly IMapper mapper;

        public AllDrinksViewModel GetAllDrinks()
        {
            var drinks = this.repository.All()
                .To<DrinkViewModel>()
                .ToList();

            var model = new AllDrinksViewModel() { Drinks = drinks };

            return model;

        }

        public async Task CreateDrink(CreateDrinkViewModel drinkModel)
        {
            var drink = this.mapper.Map<Drink>(drinkModel);

            await this.repository.AddAsync(drink);
            this.repository.SaveChanges();
        }

        public DrinkDetailsViewModel GetDrinkModel(string id)
        {
            var drink = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (drink == null)
            {
                return null;
            }

            var model = this.mapper.Map<DrinkDetailsViewModel>(drink);

            model.Calories = Math.Round(model.Calories);

            return model;
        }

        public void EditDrink(DrinkEditDeleteViewModel editModel)
        {
            var drink = this.repository.All().FirstOrDefault(x => x.Id == editModel.Id);

            if (drink == null)
            {
                return;
            }

            if (!Enum.TryParse(editModel.DrinkType, out DrinkType drinkType))
            {
                return;
            }

            drink.Name = editModel.Name;
            drink.Calories = editModel.Calories;
            drink.Description = editModel.Description;
            drink.DrinkType = drinkType;
            drink.ImageUrl = editModel.ImageUrl;
            drink.Price = editModel.Price;

            this.repository.SaveChanges();

        }

        public DrinkEditDeleteViewModel EditDeleteDrinkGetModel(string id)
        {
            var drink = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (drink == null)
            {
                return null;
            }

            var viewModel = this.mapper.Map<DrinkEditDeleteViewModel>(drink);

            return viewModel;

        }

        public void DeleteDrink(string id)
        {
            var drink = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (drink == null)
            {
                return;
            }

            this.repository.Delete(drink);
            this.repository.SaveChanges();
        }
    }
}
