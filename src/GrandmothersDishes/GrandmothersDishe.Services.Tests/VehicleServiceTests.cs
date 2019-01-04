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
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace GrandmothersDishes.Services.Tests
{
    public class VehicleServiceTests
    {
        [Fact]
        public async Task CreateVehicleShouldAddVehicle()
        {
            var vehicleRepository = new Mock<IRepository<Vehicle>>();

            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Vehicle_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var repository = new Repository<Vehicle>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateVehicleProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new VehicleService(repository , mapper);

           var firstVehicle = new Vehicle()
           {
               FuelConsumption = 4,
               HorsePowers = 322,
               ImageUrl = "imageUrl",
               Manifacturer = Manifacturer.Mercedes,
               Model = "S-class",
               Weight = 1899,
               VehicleType = VehicleType.Car,
           };


            var secondVehicle = new Vehicle()
            {
                FuelConsumption = 5,
                HorsePowers = 922,
                ImageUrl = "imageUrlllll",
                Manifacturer = Manifacturer.BMW,
                Model = "M5",
                Weight = 2600,
                VehicleType = VehicleType.Car,
            };

            var firstModel = mapper.Map<CreateVehicleViewModel>(firstVehicle);
            var secondModel = mapper.Map<CreateVehicleViewModel>(secondVehicle);

          await  service.CreateVehicle(firstModel);
          await  service.CreateVehicle(secondModel);

          Assert.Equal(2 , repository.All().Count());
        }

        [Fact]
        public void GetDeleteVehicleViewModelShouldReturnVehicleToDeleteWithViewModel()
        {
            var vehicleRepository = new Mock<IRepository<Vehicle>>();

            AutoMapperConfig.RegisterMappings(
                typeof(VehicleServiceTests).Assembly
            );

            vehicleRepository.Setup(x => x.All())
                .Returns(new List<Vehicle>()
                {
                   new Vehicle()
                   {
                       FuelConsumption = 4,
                       HorsePowers = 322,
                       ImageUrl = "imageUrl",
                       Manifacturer = Manifacturer.Mercedes,
                       Model = "S-class",
                       Weight = 1899,
                       VehicleType = VehicleType.Car,
                   }
                }.AsQueryable());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateCardProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var currentVehicle = vehicleRepository.Object.All().First();
            var mappedVehicle = mapper.Map<DeleteVehicleViewModel>(currentVehicle);

            var currentVehicleId = vehicleRepository.Object.All().FirstOrDefault().Id;

            var service = new VehicleService(vehicleRepository.Object, mapper);
            var vehicle = service.GetDeleteVihecleViewModel(currentVehicleId);
            
            mappedVehicle.Should().BeEquivalentTo(vehicle);
        }

        [Fact]
        public void GetAllVihiclesWithViewModelShouldReturnAllVehiclesWithViewModel()
        {
            var vehicleRepository = new Mock<IRepository<Vehicle>>();

            var service = new VehicleService(vehicleRepository.Object
                , null);

            vehicleRepository.Setup(x => x.All())
                .Returns(new List<Vehicle>()
                {
                    new Vehicle(),
                    new Vehicle(),
                    new Vehicle(),
                }.AsQueryable());
            

            AutoMapperConfig.RegisterMappings(
                typeof(VehicleServiceTests).Assembly
            );

            var mappedVehicles = vehicleRepository.Object.All()
                .To<VehicleViewModel>()
                .ToList();
            
            var allvehicles = service.GetAllVehiclesWithViewModel();

            mappedVehicles.Should().BeEquivalentTo(allvehicles.Vehicles);
        }

        [Fact]
        public async Task DeleteVehicleShouldRemoveVihecle()
        {
            var vehicleRepository = new Mock<IRepository<Vehicle>>();

            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Vehicle_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var repository = new Repository<Vehicle>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateVehicleProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new VehicleService(repository, mapper);

            var firstVehicle = new Vehicle()
            {
                FuelConsumption = 4,
                HorsePowers = 322,
                ImageUrl = "imageUrl",
                Manifacturer = Manifacturer.Mercedes,
                Model = "S-class",
                Weight = 1899,
                VehicleType = VehicleType.Car,
            };


            var secondVehicle = new Vehicle()
            {
                FuelConsumption = 5,
                HorsePowers = 922,
                ImageUrl = "imageUrlllll",
                Manifacturer = Manifacturer.BMW,
                Model = "M5",
                Weight = 2600,
                VehicleType = VehicleType.Car,
            };

           await dbContext.AddAsync(firstVehicle);
           await dbContext.AddAsync(secondVehicle);
            dbContext.SaveChanges();
            
            service.DeleteVehicle(secondVehicle.Id);
            
            Assert.Equal(1 , repository.All().Count());
           
        }

    }
    
}
