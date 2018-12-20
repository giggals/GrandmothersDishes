using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles
{
    public class CreateVehicleViewModel
    {
        [Required]
        [StringLength(VehicleConstants.MaxLenghtModel, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage , MinimumLength = VehicleConstants.MinLenghtModel)]
        public string Model  { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string Manifacturer { get; set; }

        [Required]
        [Range(VehicleConstants.MinHorsePowers, VehicleConstants.MaxHorsePowers)]
        public double HorsePowers { get; set; }

        [Required]
        [Range(VehicleConstants.MinFuelConsumption, VehicleConstants.MaxFuelConsumption)]
        public double FuelConsumption { get; set; }

        [Required]
        [Range(VehicleConstants.MinWeight, VehicleConstants.MaxWeight)]
        public double Weight { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
