using AutoMapper;
using GrandmothersDishes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;

namespace GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeViewModel, Employee>();
        }
    }
}
