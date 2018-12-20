using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles
{
    public class DeleteVehicleViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }
  
        public string VehicleType { get; set; }

        public string Manifacturer { get; set; }

        public int HorsePowers { get; set; }
        
        public double FuelConsumption { get; set; }

        public double Weight { get; set; }

        public string ImageUrl { get; set; }
    }
}
