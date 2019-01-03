using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class CreateVehicleProfile : Profile
    {
        public CreateVehicleProfile()
        {
            CreateMap<CreateVehicleViewModel, Vehicle>();
        }
        
    }
}
