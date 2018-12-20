using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService
{
    public interface IVehicleService
    {
        void CreateVehicle(CreateVehicleViewModel vehicleModel);
    }
}
