using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService
{
    public interface IDiscountCardService
    {
        void CreateCard(CreateCardViewModel cardModel);

        ICollection<DiscountCardViewModel> GetAllCardsWithViewModel();

        AllDiscountCardsViewModel GetAll(ICollection<DiscountCardViewModel> cards);
        
    }
}
