using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using FluentAssertions;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users;
using GrandmothersDishes.Web.ViewModels.Account;
using Moq;
using Xunit;

namespace GrandmothersDishes.Services.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public void AllUsersShouldReturnAllOfThem()
        {
            var usersRepository = new Mock<IRepository<GrandMothersUser>>();

            usersRepository.Setup(x => x.All())
                .Returns(new List<GrandMothersUser>()
                    {
                        new GrandMothersUser(),
                        new GrandMothersUser(),
                        new GrandMothersUser()
                    }
                    .AsQueryable());

            var service = new UsersService(usersRepository.Object , null);

            var expected = usersRepository.Object.All();

            var result = service.AllUsers();
            
            expected.Should().BeEquivalentTo(result);
            Assert.Equal(expected.Count() , result.Count());
        }

        [Fact]
        public void MapFromRegisterViewModelShouldMapToGrandmothersUser()
        {

            var registerViewModel = new RegisterViewModel()
            {
                City = "Sofiq",
                ConfirmPassword = "123",
                Email = "Stamat@abv.bg",
                HomeAddress = "Pirin 11",
                Password = "123",
                Username = "giggals"
            };

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RegisterProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new UsersService(null, mapper);

            var result = service.MapFromRegisterViewModel(registerViewModel);

            var expected = new GrandMothersUser()
            {
                City = "Sofiq",
                Email = "Stamat@abv.bg",
                HomeAddress = "Pirin 11",
                UserName = "giggals"
            };

            expected.UserName.Should().Be(result.UserName);
            expected.City.Should().Be(result.City);
            expected.Email.Should().Be(result.Email);
            expected.HomeAddress.Should().Be(result.HomeAddress);

        }
    }
}
