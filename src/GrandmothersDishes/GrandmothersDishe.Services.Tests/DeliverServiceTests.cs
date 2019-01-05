using Moq;
using System;
using System.Collections.Generic;
using GrandmothersDishes.Models;
using Xunit;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using Microsoft.EntityFrameworkCore;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern;
using AutoMapper;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDIshes.DeliverService;
using System.Threading.Tasks;
using FluentAssertions;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;
using System.Linq;
using Delivery = GrandmothersDishes.Models.Delivery;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;

namespace GrandmothersDishes.Services.Tests
{
    public class DeliverServiceTests
    {
        [Fact]
        public async Task DeliverShouldCreateDeliver()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Deliver_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var deliveryRepository = new Repository<Delivery>(dbContext);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);

            var ordersRepository = new Repository<Order>(dbContext);

            var dishRepository = new Repository<Dish>(dbContext);

            var drinkRepository = new Repository<Drink>(dbContext);

            var orderDrinskRepository = new Repository<OrderDrinks>(dbContext);

            var orderDishesRepository = new Repository<OrderDishes>(dbContext);

            var discountCardRepository = new Repository<DiscountCard>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeliverProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var cardService = new DiscountCardService(discountCardRepository, null);

            var orderService = new OrderService(ordersRepository,
                dishRepository, drinkRepository,
                usersRepository, orderDrinskRepository,
                orderDishesRepository,
                discountCardRepository,
                 cardService
                );

            var deliveryService = new DeliverService(deliveryRepository, orderService, usersRepository, mapper);

            var user = new GrandMothersUser()
            {
                UserName = "test",
                FirstName = "Nikolay",
                LastName = "Dimitrov",
                City = "Sofiq"
            };

            await dbContext.Users.AddAsync(user);
            dbContext.SaveChanges();

            var delivery = new DeliverViewModel()
            {
                Address = "asd",
                DeliveryType = DeliveryType.ByMotorcycle.ToString(),
            };

            var result = deliveryService.Deliver(delivery, user.UserName);

            delivery.Address.Should().BeEquivalentTo(result.Result.Address);
            delivery.DeliveryType.Should().BeEquivalentTo(result.Result.DeliveryType.ToString());

        }

        [Fact]
        public async Task GetDeliveryDetailsShouldReturnDeliveryWithViewModel()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "DeliverDetailsModel_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var deliveryRepository = new Repository<Delivery>(dbContext);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);


            var user = new GrandMothersUser()
            {
                UserName = "test"
            };


            var expectedDelivery = new Delivery()
            {
                Address = "asd",
                User = user,
            };

            await dbContext.AddAsync(user);

            await dbContext.AddAsync(expectedDelivery);

            dbContext.SaveChanges();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeliverProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var deliveryService = new DeliverService(deliveryRepository, null, usersRepository, mapper);

            var result = deliveryService.GetDeliveryDetails("test", expectedDelivery.Id);

            expectedDelivery.Address.Should().BeEquivalentTo(result.Address);

            expectedDelivery.User.UserName.Should().BeEquivalentTo(result.User);
        }

        [Fact]
        public async Task AllUserDeliveriesShouldReturnDeliveriesOfTheCurrentUser()
        {
            var deliveriesRepository = new Mock<IRepository<Delivery>>();

            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "DeliverDetailsModel_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var usersRepository = new Repository<GrandMothersUser>(dbContext);


            var user = new GrandMothersUser()
            {
                UserName = "test"
            };

            await dbContext.Users.AddAsync(user);

            deliveriesRepository.Setup(x => x.All())
                .Returns(new List<Delivery>()
                {
                    new Delivery() {User =user},
                    new Delivery() {User = user},
                    new Delivery() {User = user}
                }
                    .AsQueryable());

            AutoMapperConfig.RegisterMappings(
                typeof(DeliverServiceTests).Assembly
            );
            
            var service = new DeliverService(deliveriesRepository.Object, null, usersRepository, null);

            var result = service.AllUserDeliveries(user.UserName);

            Assert.Equal(3 , result.Deliveries.Count);
        }
    }
}
