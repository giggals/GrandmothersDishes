
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;

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

        public ICollection<DiscountCardViewModel> GetAllCardsWithViewModel()
        {
            var cards = this.repository.All()
                .To<DiscountCardViewModel>()
                .ToList();

            return cards;
        }

        public AllDiscountCardsViewModel GetAll(ICollection<DiscountCardViewModel> cards)
        {
            var allCards = new AllDiscountCardsViewModel() { DiscountCards = cards };

            return allCards;
        }


    }
}
