using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.HomeService;
using Moq;
using Xunit;

namespace GrandmothersDishes.Services.Tests
{
    public class HomeServiceTests
    {
        [Fact]
        public void AllEmployeesShouldReturnAllOfThem()
        {
            var employeeRepository = new Mock<IRepository<Employee>>();

            employeeRepository.Setup(x => x.All())
                .Returns(new List<Employee>()
                    {
                        new Employee(),
                        new Employee()
                    }
                    .AsQueryable());

            AutoMapperConfig.RegisterMappings(
                typeof(VehicleServiceTests).Assembly
            );

            var service = new HomeService(null, employeeRepository.Object);

            var result = service.AllEmployees();
            var expected = employeeRepository.Object.All();

            expected.Should().BeEquivalentTo(result.Employees);
        }


        [Fact]
        public void AllDishesShouldReturnAllProducts()
        {
            var dishRepository = new Mock<IRepository<Dish>>();

            dishRepository.Setup(x => x.All())
                .Returns(new List<Dish>()
                    {
                        new Dish(),
                        new Dish()
                    }
                    .AsQueryable());

            AutoMapperConfig.RegisterMappings(
                typeof(VehicleServiceTests).Assembly
            );

            var service = new HomeService(dishRepository.Object, null);

            var result = service.AllDishes();
            var expected = dishRepository.Object.All();

            expected.Should().BeEquivalentTo(result.Products);
        }
    }
}
