using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> repository;
        private readonly IMapper mapper;

        public VehicleService(IRepository<Vehicle> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void CreateVehicle(CreateVehicleViewModel vehicleModel)
        {
            var vehicle = this.mapper.Map<Vehicle>(vehicleModel);

            if (vehicle == null)
            {
                return;
            }

            this.repository.AddAsync(vehicle);
            this.repository.SaveChanges();

        }
    }
}
