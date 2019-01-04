using AutoMapper;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern;
using GrandmothersDishes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;
using Xunit;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using FluentAssertions;

namespace GrandmothersDishes.Services.Tests
{
    public class FoodServiceTests
    {
        [Fact]
        public void CreateDishShouldCreateDishAndAddItToDatabase()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Dish_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var dishesRepository = new Repository<Dish>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DishProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new FoodService(dishesRepository, mapper);

            var firstModel = new CreateDishViewModel()
            {
                Description = "first",
                ImageUrl = "firstImage",
                Name = "FirstProduct",
                Price = 1,
                Calories = 11,
                DishType = DishType.JunkFood.ToString(),
            };

            var secondModel = new CreateDishViewModel()
            {
                Description = "second",
                ImageUrl = "secondImage",
                Name = "SecondProduct",
                Price = 2,
                Calories = 22,
                DishType = DishType.JunkFood.ToString(),
            };

            service.CreateDish(firstModel);
            service.CreateDish(secondModel);

            Assert.Equal(2, dbContext.Dishes.Count());
        }

        [Fact]
        public async Task GetDishDetailsShouldReturnCurrentDishWithViewModel()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Dish_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var dishesRepository = new Repository<Dish>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DishProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new FoodService(dishesRepository, mapper);

            var dish = new Dish()
            {
                Description = "first",
                ImageUrl = "firstImage",
                Name = "FirstProduct",
                Price = 1,
                Calories = 11,
                DishType = DishType.Salad,
            };

            await dbContext.Dishes.AddAsync(dish);
            dbContext.SaveChanges();

            var expected = new DetailsDishViewModel()
            {
                Id = dish.Id,
                Calories = 11,
                Description = "first",
                DishType = DishType.Salad.ToString(),
                ImageUrl = "firstImage",
                Price = 1,
                Name = "FirstProduct",

            };

            var result = service.GetDishDetails(dish.Id);

            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task EditDishShouldReturnCurrentDishWithEditedProperties()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_Dish_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var dishesRepository = new Repository<Dish>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DishProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new FoodService(dishesRepository, mapper);

            var dish = new Dish()
            {
                Description = "dish",
                ImageUrl = "dishImage",
                Name = "dishProduct",
                Price = 1,
                Calories = 11,
                DishType= DishType.Salad,
            };

            await dbContext.Dishes.AddAsync(dish);
            dbContext.SaveChanges();

            var model = new UpdateDeleteViewModel()
            {
                Id = dish.Id,
                Description = "edited",
                ImageUrl = "editImage",
                Name = "editName",
                Price = 100,
                Calories = 111,
                DishType = DishType.Salad.ToString()
            };

            service.EditDish(model);

            dish.Name.Should().Be("editName");
            dish.Description.Should().Be("edited");
            dish.ImageUrl.Should().Be("editImage");
            dish.Price.Should().Be(100);
            dish.Calories.Should().Be(111);
            dish.DishType.Should().Be(DishType.Salad);
        }

        [Fact]
        public async Task EditDeleteDishGetModelShouldReturnDishWithEditDeleteViewModel()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_Dish_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var dishesRepository = new Repository<Dish>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DishProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new FoodService(dishesRepository, mapper);

            var dish = new Dish()
            {
                Description = "dish",
                ImageUrl = "dishImage",
                Name = "dishProduct",
                Price = 1,
                Calories = 11,
                DishType = DishType.Salad,
            };

            await dbContext.Dishes.AddAsync(dish);
            dbContext.SaveChanges();

            var expected = new UpdateDeleteViewModel()
            {
                Id = dish.Id,
                Description = "dish",
                ImageUrl = "dishImage",
                Name = "dishProduct",
                Price = 1,
                Calories = 11,
                DishType = DishType.Salad.ToString(),
            };

            var result = service.EditDeleteDishGetModel(dish.Id);

            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task DeleteDishShouldDeleteDishAndRemoveItFromDatabase()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_Dish_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var dishesRepository = new Repository<Dish>(dbContext);

            var service = new FoodService(dishesRepository, null);

            var firstDish = new Dish()
            {
                Description = "firstDish",
                ImageUrl = "firstImage",
                Name = "firstNameproduct",
                Price = 1,
                Calories = 1,
                DishType = DishType.Cakes,
            };

            var secondDish = new Dish()
            {
                Description = "secondDish",
                ImageUrl = "secondImage",
                Name = "SecondProduct",
                Price = 2,
                Calories = 2,
                DishType = DishType.Salad,
            };

            await dbContext.AddAsync(firstDish);
            await dbContext.AddAsync(secondDish);
            dbContext.SaveChanges();

            service.DeleteDish(secondDish.Id);

            Assert.Equal(1 , dbContext.Dishes.Count());
        }
    }
}
