using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using Moq;
using Xunit;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions.Impl;
using GrandmothersDishes.Data;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using GrandmothersDishes.Data.RepositoryPattern;

namespace GrandmothersDishe.Services.Tests
{
    public class DiscountServiceTests
    {
        [Fact]
        public void GetAllCardsWithViewModelShouldReturnAllOfThem()
        {
            var discountCardsRepository = new Mock<IRepository<DiscountCard>>();
            
            AutoMapperConfig.RegisterMappings(
                typeof(DiscountServiceTests).Assembly
            );

            discountCardsRepository.Setup(x => x.All())
                .Returns(new List<DiscountCard>()
                {
                    new DiscountCard(),
                    new DiscountCard(),
                    new DiscountCard(),
                }.AsQueryable());
            
            var all = discountCardsRepository.Object.All().Count();
            
            var service = new DiscountCardService(discountCardsRepository.Object, null);
            
            var cards = service.GetAllCardsWithViewModel();
            
            Assert.Equal(cards.Count , all);
        }

        [Fact]
        public void CreateCardShouldAddCard()
        {
            var options = new DbContextOptionsBuilder<GrandmothersDishesDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Card_Database")
                .Options;

            var dbContext = new GrandmothersDishesDbContext(options);
            
            var repository = new Repository<DiscountCard>(dbContext);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateCardProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new DiscountCardService(repository , mapper);

            var card = new CreateCardViewModel()
            {
               Description = "Card",
              DiscountPercentage = 5,
               DiscountType = DiscountType.Normal.ToString(),
               ImageUrl = "..."
            };

            var secondCard = new CreateCardViewModel()
            {
                Description = "SecondCard",
                DiscountPercentage = 10,
                DiscountType = DiscountType.Normal.ToString(),
                ImageUrl = ".222222."
            };

            var mappedCard = mapper.Map<DiscountCard>(card);
            var mappedSecondCard = mapper.Map<DiscountCard>(secondCard);

            service.CreateCard(card);
            service.CreateCard(secondCard);

            Assert.Equal(2 ,dbContext.DiscountCards.Count());
        }

        [Fact]
        public void GetAllShouldReturnAllDiscountCardsWithViewModel()
        {
            var discountCardsRepository = new Mock<IRepository<DiscountCard>>();
            
            var service = new DiscountCardService(discountCardsRepository.Object 
                ,null);

           var cards = discountCardsRepository.Setup(x => x.All())
                .Returns(new List<DiscountCard>()
                {
                    new DiscountCard(),
                    new DiscountCard(),
                    new DiscountCard(),
                }.AsQueryable());

            AutoMapperConfig.RegisterMappings(
                typeof(DiscountServiceTests).Assembly
            );
            
            var mappedCards = discountCardsRepository.Object.All()
                .To<DiscountCardViewModel>()
                .ToList();
            

            var allCards = service.GetAll(mappedCards);

            Assert.Equal(mappedCards, allCards.DiscountCards);
            
        }
    }
}
