using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern;
using GrandmothersDishes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.EmployeeService;
using Xunit;
using AutoMapper;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles;
using System.Threading.Tasks;
using FluentAssertions;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;
using Moq;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;

namespace GrandmothersDishes.Services.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void CreateEmployeeShouldAddEmployee()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Employee_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var employeeRepository = new Repository<Employee>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmployeeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new EmployeeService(employeeRepository, mapper);

            var firstModel = new CreateEmployeeViewModel()
            {
                Description = "description",
                EmployeeType = EmployeeType.Chef.ToString(),
                FirstName = "Ivan",
                Gender = Gender.Male.ToString(),
                ImageUrl = "imageurl",
                LastName = "Dimitrov",
                Salary = 4500,
                WorkingHours = 8,
            };

            var secondModel = new CreateEmployeeViewModel()
            {
                Description = "description",
                EmployeeType = EmployeeType.Chef.ToString(),
                FirstName = "Ivan",
                Gender = Gender.Male.ToString(),
                ImageUrl = "imageurl",
                LastName = "Dimitrov",
                Salary = 4500,
                WorkingHours = 8,
            };

            service.CreateEmployee(firstModel);
            service.CreateEmployee(secondModel);

            Assert.Equal(2, dbContext.Employees.Count());
        }


        [Fact]
        public void GetAllEmployeesShouldReturnAllEmployees()
        {
            var employeeRepository = new Mock<IRepository<Employee>>();

            employeeRepository.Setup(x => x.All()).Returns(new List<Employee>()
            {
                new Employee(),
                new Employee(),
                new Employee(),
                new Employee(),
                new Employee(),
            }.AsQueryable());

            var service = new EmployeeService(employeeRepository.Object, null);

            AutoMapperConfig.RegisterMappings(
                typeof(EmployeeServiceTests).Assembly
            );

            var result = service.GetAllEmployees();
            var expected = employeeRepository.Object.All();

            Assert.Equal(expected.Count(), result.Employees.Count);

        }

        [Fact]
        public async Task DissmisShouldRemoveEmployee()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Employee_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);

            var employeeRepository = new Repository<Employee>(dbContext);

            var service = new EmployeeService(employeeRepository, null);

            var firstEmployee = new Employee()
            {
                Description = "description",
                EmployeeType = EmployeeType.Chef,
                FirstName = "Ivan",
                Gender = Gender.Male,
                ImageUrl = "imageurl",
                LastName = "Dimitrov",
                Salary = 4500,
                WorkingHours = 8,
            };
            
            var secondEmployee = new Employee()
            {
                Description = "descriptionNNN",
                EmployeeType = EmployeeType.Chef,
                FirstName = "Nikolay",
                Gender = Gender.Male,
                ImageUrl = "imageurl....",
                LastName = "Kovachev",
                Salary = 9500,
                WorkingHours = 4,
            };

            var thirdEmployee = new Employee()
            {
                Description = "3descriptionNNN",
                EmployeeType = EmployeeType.Chef,
                FirstName = "Nikolay3",
                Gender = Gender.Male,
                ImageUrl = "3imageurl....",
                LastName = "3Kovachev",
                Salary = 3333,
                WorkingHours = 3,
            };

            await dbContext.Employees.AddAsync(firstEmployee);
            await dbContext.Employees.AddAsync(secondEmployee);
            await dbContext.Employees.AddAsync(thirdEmployee);
            dbContext.SaveChanges();

            service.DismissEmployee(firstEmployee.Id);

            Assert.Equal(2 , dbContext.Employees.Count());
        }

    }
}
