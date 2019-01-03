using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class CreateCardProfile : Profile
    {
        public CreateCardProfile()
        {
            CreateMap<CreateCardViewModel, DiscountCard>();
        }
    }
}
