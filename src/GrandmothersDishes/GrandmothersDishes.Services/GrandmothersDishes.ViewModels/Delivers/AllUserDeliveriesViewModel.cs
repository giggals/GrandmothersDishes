using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers
{
    public class AllUserDeliveriesViewModel
    {
        public ICollection<DeliveryAllViewModel> Deliveries { get; set; }
    }
}
