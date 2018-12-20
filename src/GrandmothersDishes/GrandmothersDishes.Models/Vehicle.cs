using GrandmothersDishes.Models.Enums;
using System;

namespace GrandmothersDishes.Models
{
    public class Vehicle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Manifacturer Manifacturer { get; set; }

        public string Model { get; set; }

        public VehicleType VehicleType { get; set; }

        public int HorsePowers { get; set; }

        public decimal FuelConsumption { get; set; }

        public double Weight { get; set; }

        public string ImageUrl { get; set; }

    }
}
