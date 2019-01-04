using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class DeliverProfile : Profile
    {
        public DeliverProfile()
        {
            CreateMap<DeliverViewModel, Delivery>();
            CreateMap<Delivery, DeliverViewModel>();
            CreateMap<Delivery, DeliveryAllViewModel>();
        }
    }
}
