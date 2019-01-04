using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace GrandmothersDishes.Services.Tests
{
    public class DrinkServiceTests
    {
        [Fact]
        public async Task CreateDrinkShoulCreateDrink()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Drink_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var drinkRepository = new Repository<Drink>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DrinkProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new DrinksService(drinkRepository, mapper);

            var firstModel = new CreateDrinkViewModel()
            {
                Description = "first",
                ImageUrl = "firstImage",
                Name = "FirstProduct",
                Price = 1,
                Calories = 11,
                DrinkType = DrinkType.Alcohol.ToString(),
            };

            var secondModel = new CreateDrinkViewModel()
            {
                Description = "second",
                ImageUrl = "secondImage",
                Name = "SecondProduct",
                Price = 2,
                Calories = 22,
                DrinkType = DrinkType.Alcohol.ToString(),
            };

            await service.CreateDrink(firstModel);
            await service.CreateDrink(secondModel);

            Assert.Equal(2, dbContext.Drinks.Count());
        }

        [Fact]
        public void GetAllDrinksShouldReturnAllDrinks()
        {
            var drinkRepository = new Mock<IRepository<Drink>>();

            drinkRepository.Setup(x => x.All())
                .Returns(new List<Drink>()
                    {
                      new Drink(),
                      new Drink(),
                      new Drink(),
                      new Drink(),
                      new Drink(),
                    }
                    .AsQueryable);

            var service = new DrinksService(drinkRepository.Object, null);

            AutoMapperConfig.RegisterMappings(
                typeof(DrinkServiceTests).Assembly
            );

            var result = service.GetAllDrinks();
            var expected = drinkRepository.Object.All();

            Assert.Equal(expected.Count(), result.Drinks.Count);
        }

        [Fact]
        public async Task GetDrinkModelShouldReturnCurrentDrinkWithViewModel()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "DrinkWithViewModel_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var drinkRepository = new Repository<Drink>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DrinkProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new DrinksService(drinkRepository, mapper);

            var drink = new Drink()
            {
                Description = "first",
                ImageUrl = "firstImage",
                Name = "FirstProduct",
                Price = 1,
                Calories = 11,
                DrinkType = DrinkType.Alcohol,
            };

            await dbContext.Drinks.AddAsync(drink);
            dbContext.SaveChanges();

            var expected = new DrinkDetailsViewModel()
            {
                Id = drink.Id,
                Calories = 11,
                Description = "first",
                DrinkType = DrinkType.Alcohol.ToString(),
                ImageUrl = "firstImage",
                Price = 1,
                Name = "FirstProduct",

            };

            var result = service.GetDrinkModel(drink.Id);

            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task EditDrinkShouldEditDrink()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "EditDrink_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var drinkRepository = new Repository<Drink>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DrinkProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new DrinksService(drinkRepository, mapper);

            var drink = new Drink()
            {
                Description = "drink",
                ImageUrl = "drinkImage",
                Name = "drinkProduct",
                Price = 1,
                Calories = 11,
                DrinkType = DrinkType.Alcohol,
            };

            await dbContext.Drinks.AddAsync(drink);
            dbContext.SaveChanges();

            var model = new DrinkEditDeleteViewModel()
            {
                Id = drink.Id,
                Description = "edited",
                ImageUrl = "editImage",
                Name = "editName",
                Price = 100,
                Calories = 111,
                DrinkType = DrinkType.Beer.ToString(),
            };

            service.EditDrink(model);

            drink.Name.Should().Be("editName");
            drink.Description.Should().Be("edited");
            drink.ImageUrl.Should().Be("editImage");
            drink.Price.Should().Be(100);
            drink.Calories.Should().Be(111);
            drink.DrinkType.Should().Be(DrinkType.Beer);
        }

        [Fact]
        public async Task EditDeleteDrinkGetModelShouldReturnDrinkWithEditDeleteViewModel()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "EditDeleteViewModel_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var drinkRepository = new Repository<Drink>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DrinkProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new DrinksService(drinkRepository, mapper);

            var drink = new Drink()
            {
                Description = "editDelete",
                ImageUrl = "editeDeleteImage",
                Name = "editDeleteProduct",
                Price = 12,
                Calories = 22,
                DrinkType = DrinkType.Juice,
            };

            await dbContext.Drinks.AddAsync(drink);
            dbContext.SaveChanges();

            var expected = new DrinkDetailsViewModel()
            {
                Id = drink.Id,
                Calories = 22,
                Description = "editDelete",
                DrinkType = DrinkType.Juice.ToString(),
                ImageUrl = "editeDeleteImage",
                Price = 12,
                Name = "editDeleteProduct",

            };

            var result = service.EditDeleteDrinkGetModel(drink.Id);

            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task DeleteDrinkShouldDeleteDrinkFromDatabase()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "EditDeleteViewModel_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var drinkRepository = new Repository<Drink>(dbContext);

            var service = new DrinksService(drinkRepository, null);

            var firstDrink = new Drink()
            {
                Description = "firstDrink",
                ImageUrl = "firstImage",
                Name = "firstNameproduct",
                Price = 1,
                Calories = 1,
                DrinkType = DrinkType.Juice,
            };

            var secondDrink = new Drink()
            {
                Description = "secondDrink",
                ImageUrl = "secondImage",
                Name = "SecondProduct",
                Price = 2,
                Calories = 2,
                DrinkType = DrinkType.EnergyDrink,
            };

            await dbContext.AddAsync(firstDrink);
            await dbContext.AddAsync(secondDrink);
            dbContext.SaveChanges();

            service.DeleteDrink(secondDrink.Id);

            Assert.Equal(1, dbContext.Drinks.Count());
        }
    }
}
