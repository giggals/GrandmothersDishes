using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class Deliveries
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Address { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public User User { get; set; }

    }
}
