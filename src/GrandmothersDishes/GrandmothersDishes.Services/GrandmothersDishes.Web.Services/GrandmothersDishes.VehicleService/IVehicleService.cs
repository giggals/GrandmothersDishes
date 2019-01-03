using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService
{
    public interface IVehicleService
    {
         Task CreateVehicle(CreateVehicleViewModel vehicleModel);

        AllVehiclesViewModel GetAllVehiclesWithViewModel();

        DeleteVehicleViewModel GetDeleteVihecleViewModel(string id);

        void DeleteVehicle(string id);
    }
}
