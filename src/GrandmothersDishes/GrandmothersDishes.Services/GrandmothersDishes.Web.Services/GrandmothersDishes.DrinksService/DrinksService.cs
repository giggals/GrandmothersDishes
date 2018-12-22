using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService
{
    public class DrinksService : IDrinkService
    {
        public DrinksService(IRepository<Drink> drinksRepository,
            IMapper mapper)
        {
            this.drinksRepository = drinksRepository;
            this.mapper = mapper;
        }

        private readonly IRepository<Drink> drinksRepository;
        private readonly IMapper mapper;

        public AllDrinksViewModel GetAllDrinks(DrinkViewModel drinkModel)
        {
            var drinks = this.drinksRepository.All()
                .To<DrinkViewModel>()
                .ToList();

            var model = new AllDrinksViewModel() {Drinks = drinks};

            return model;

        }

        public void CreateDrink(CreateDrinkViewModel drinkModel)
        {
            var drink = this.mapper.Map<Drink>(drinkModel);

            this.drinksRepository.AddAsync(drink);
            this.drinksRepository.SaveChanges();
        }
    }
}
