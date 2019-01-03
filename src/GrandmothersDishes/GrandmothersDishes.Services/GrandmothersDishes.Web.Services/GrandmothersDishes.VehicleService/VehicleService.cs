using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
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

        public async Task CreateVehicle(CreateVehicleViewModel vehicleModel)
        {
            var vehicle = this.mapper.Map<Vehicle>(vehicleModel);

            if (vehicle == null)
            {
                return;
            }

           await this.repository.AddAsync(vehicle);
            this.repository.SaveChanges();
        }

        public AllVehiclesViewModel GetAllVehiclesWithViewModel()
        {
            var vehicles = this.repository.All().To<VehicleViewModel>()
                .ToList();

            var model = new AllVehiclesViewModel() {Vehicles = vehicles};

            return model;
        }

        public DeleteVehicleViewModel GetDeleteVihecleViewModel(string id)
        {
            var vehicle = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (vehicle == null)
            {
                return null;
            }

            var viewModel = this.mapper.Map<DeleteVehicleViewModel>(vehicle);

            return viewModel;
        }

        public void DeleteVehicle(string id)
        {
            var vehicle = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (vehicle == null)
            {
                return;
            }

            this.repository.Delete(vehicle);
            this.repository.SaveChanges();
        }
    }
}
