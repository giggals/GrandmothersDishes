using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService
{
    public class DiscountCardService : IDiscountCardService
    {
        public DiscountCardService(IRepository<DiscountCard> repository,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.mapper = mapper;
           
        }

        private readonly IRepository<DiscountCard> repository;
        private readonly IMapper mapper;
      

        public void CreateCard(CreateCardViewModel cardModel)
        {
            var card = this.mapper.Map<DiscountCard>(cardModel);

            this.repository.AddAsync(card);
            this.repository.SaveChanges();
        }

        public AllDiscountCardsViewModel GetAllCards()
        {
            var cards = this.repository.All()
                .To<DiscountCardViewModel>()
                .ToList();

            var allCards = new AllDiscountCardsViewModel(){DiscountCards = cards };

            return allCards;
        }
        
        
    }
}
