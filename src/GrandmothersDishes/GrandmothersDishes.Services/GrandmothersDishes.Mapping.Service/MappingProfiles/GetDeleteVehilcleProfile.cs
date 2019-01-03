using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using GrandmothersDishes.Models;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class GetDeleteVehilcleProfile : Profile
    {
        public GetDeleteVehilcleProfile()
        {
            CreateMap<Vehicle, DeleteVehicleViewModel>();
        }
    }
}
