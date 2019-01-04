using AutoMapper;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using Xunit;
using Moq;
using OrderDrinks = GrandmothersDishes.Models.OrderDrinks;

namespace GrandmothersDishes.Services.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task MakeOrderShouldCreateOrder()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Order_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var orderRepository = new Repository<Order>(dbContext);
            var dishRepository = new Repository<Dish>(dbContext);
            var drinkRepository = new Repository<Drink>(dbContext);
            var usersRepository = new Repository<GrandMothersUser>(dbContext);


            var service = new OrderService(orderRepository, dishRepository, drinkRepository, usersRepository, null, null, null, null);

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

            var user = new GrandMothersUser()
            {
                UserName = "testUserName"
            };

            await dbContext.Users.AddAsync(user);

            dbContext.SaveChanges();

            var result = service.MakeOrder(drink.Id, user.UserName, 2);
            var expected = new Order()
            {
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            var orderDrink = new OrderDrinks()
            {
                Drink = drink,
                Order = expected,
                Quantity = 2,
            };

            expected.Drinks.Add(orderDrink);
            expected.User = user;

            Assert.Equal(1, dbContext.Orders.Count());
        }

        [Fact]
        public async Task GetCurrentUserShouldReturnCurrentUser()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCurrentUser_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);

            var service = new OrderService(null, null, null, usersRepository, null, null, null, null);

            var user = new GrandMothersUser()
            {
                UserName = "giggals",
            };

            await dbContext.Users.AddAsync(user);
            dbContext.SaveChanges();

            var result = service.GetCurrentUser(user.UserName);

            user.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetMyActiveDishOrdersShouldReturnCurrentUserActiveDishOrders()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMyActiveDishOrders_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);
            var orderRepository = new Repository<Order>(dbContext);
            var orderDishesRepository = new Repository<OrderDishes>(dbContext);

            var service = new OrderService(orderRepository, null, null, usersRepository, null, orderDishesRepository, null, null);

            var user = new GrandMothersUser()
            {
                UserName = "giggals",
            };

            var firstOrder = new Order()
            {
                User = user,
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            var secondOrder = new Order()
            {
                User = user,
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            await dbContext.Orders.AddAsync(firstOrder);
            await dbContext.Orders.AddAsync(secondOrder);
            dbContext.SaveChanges();

            var firstDishOrder = new OrderDishes()
            {
                Dish = new Dish(),
                Order = firstOrder,
                Quantity = 1,
            };

            var secondDishOrder = new OrderDishes()
            {
                Dish = new Dish(),
                Order = secondOrder,
                Quantity = 2,
            };

            await dbContext.OrderDishes.AddAsync(firstDishOrder);
            await dbContext.OrderDishes.AddAsync(secondDishOrder);
            dbContext.SaveChanges();

            var result = service.GetMyActiveDishOrders(user.UserName);

            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task GetMyActiveDrinksOrdersShouldReturnCurrentUserActiveDrinkOrders()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMyActiveDrinksOrders_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);
            var orderRepository = new Repository<Order>(dbContext);
            var orderDrinksRepository = new Repository<OrderDrinks>(dbContext);

            var service = new OrderService(orderRepository, null, null, usersRepository, orderDrinksRepository, null, null, null);

            var user = new GrandMothersUser()
            {
                UserName = "giggals",
            };

            var firstOrder = new Order()
            {
                User = user,
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            var secondOrder = new Order()
            {
                User = user,
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            var thirddOrder = new Order()
            {
                User = user,
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            await dbContext.Orders.AddAsync(firstOrder);
            await dbContext.Orders.AddAsync(secondOrder);
            await dbContext.Orders.AddAsync(thirddOrder);
            dbContext.SaveChanges();

            var firstDrinkOrder = new OrderDrinks()
            {
                Drink = new Drink(),
                Order = firstOrder,
                Quantity = 1,
            };

            var secondDrinkOrder = new OrderDrinks()
            {
                Drink = new Drink(),
                Order = secondOrder,
                Quantity = 2,
            };

            var thirdDrinkOrder = new OrderDrinks()
            {
                Drink = new Drink(),
                Order = thirddOrder,
                Quantity = 3,
            };

            await dbContext.OrderDrinks.AddAsync(firstDrinkOrder);
            await dbContext.OrderDrinks.AddAsync(secondDrinkOrder);
            await dbContext.OrderDrinks.AddAsync(thirdDrinkOrder);
            dbContext.SaveChanges();

            var result = service.GetMyActiveDrinksOrders("giggals");

            Assert.Equal(3, result.Count());
        }


        [Fact]
        public async Task CountTotalPriceShouldCountPriceOfallProductsWithDiscount()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "CalculatePrice_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);

            var discountCardRepository = new Repository<DiscountCard>(dbContext);

            var service = new OrderService(null, null,
                null, usersRepository, null, null, discountCardRepository, null);

            decimal normalTotal = 100;
            decimal normalDiscountDiff = 0;

            decimal vipTotal = 250;
            decimal vipDiscountDiff = 0;

            decimal secondVipTotal = 1000;
            decimal secondVipDiscountDiff = 0;

            var user = new GrandMothersUser()
            {
                UserName = "test",
            };

            await dbContext.Users.AddAsync(user);
            dbContext.SaveChanges();

            var normalCard = new DiscountCard()
            {
                Description = "asd",
                DiscountPercentage = 10,
                DiscountType = DiscountType.Normal,
            };

            var vipCard = new DiscountCard()
            {
                Description = "asd",
                DiscountPercentage = 15,
                DiscountType = DiscountType.VIP,
            };

            await dbContext.DiscountCards.AddAsync(normalCard);
            await dbContext.DiscountCards.AddAsync(vipCard);
            dbContext.SaveChanges();


            service.CountTotalPrice(ref normalTotal, user, ref normalDiscountDiff);


            service.CountTotalPrice(ref vipTotal, user, ref vipDiscountDiff);

            service.CountTotalPrice(ref secondVipTotal, user, ref secondVipDiscountDiff);

            Assert.Equal(90, normalTotal);
            Assert.Equal(212.5M, vipTotal);
            Assert.Equal(850, secondVipTotal);
        }

        [Fact]
        public async Task RedirectShouldReturnFoodsOrDrinks()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Redirect_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var drinkRepository = new Repository<Drink>(dbContext);

            var dishRepository = new Repository<Dish>(dbContext);

            var service = new OrderService(null, dishRepository,
                drinkRepository, null, null, null, null, null);

            var drink = new Drink()
            {
                Name = "Drinkkkk",
            };

            await dbContext.Drinks.AddAsync(drink);
            dbContext.SaveChanges();

            var drinkId = drink.Id;

            var result = service.Redirect(drinkId);
            var secondResult = service.Redirect("");

            Assert.Equal("Drinks", result);
            Assert.Equal("Foods", secondResult);

        }

        [Fact]
        public async Task GetAllActiveOrdersShouldReturnAllActiveOrdersOfCurrentUser()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMyActiveOrders_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var ordersRepository = new Repository<Order>(dbContext);
            var usersRepository = new Repository<GrandMothersUser>(dbContext);

            var drinkRepository = new Repository<Drink>(dbContext);
            var orderDrinkRepository = new Repository<OrderDrinks>(dbContext);


            var dishRepository = new Repository<Dish>(dbContext);
            var orderDishRepository = new Repository<OrderDishes>(dbContext);

            var service = new OrderService(null, dishRepository,
                drinkRepository, usersRepository, orderDrinkRepository, orderDishRepository, null, null);

            var user = new GrandMothersUser()
            {
                UserName = "testUsername",
            };

            await dbContext.AddAsync(user);
            dbContext.SaveChanges();

            var dish = new Dish()
            {
                Name = "testDishshshs",
            };

            await dbContext.Dishes.AddAsync(dish);
            dbContext.SaveChanges();

            var firstOrder = new Order()
            {
                User = user,
                Status = Status.Active,
            };

            var drink = new Drink()
            {
                Name = "testDrink",
            };

            await dbContext.Drinks.AddAsync(drink);
            dbContext.SaveChanges();

            var secondOrder = new Order()
            {
                User = user,
                Status = Status.Active,
            };

            await dbContext.Orders.AddAsync(firstOrder);
            await dbContext.Orders.AddAsync(secondOrder);
            dbContext.SaveChanges();

            var orderDish = new OrderDishes()
            {
                Dish = dish,
                Order = firstOrder,
                Quantity = 1,
            };

            var orderDrink = new OrderDrinks()
            {
                Drink = drink,
                Order = secondOrder,
                Quantity = 1,
            };

            await dbContext.OrderDishes.AddAsync(orderDish);
            await dbContext.OrderDrinks.AddAsync(orderDrink);
            dbContext.SaveChanges();
            
            var result = service.GetAllActiveOrders(user.UserName);

            Assert.Equal(2, result.DishesOrders.Count + result.DrinksOrders.Count);
        }


    }
}
