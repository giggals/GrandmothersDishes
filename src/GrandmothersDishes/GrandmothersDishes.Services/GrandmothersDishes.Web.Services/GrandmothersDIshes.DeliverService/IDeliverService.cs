using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDIshes.DeliverService
{
    public interface IDeliverService
    {
        Delivery Deliver(DeliverViewModel deliverModel, string username);

        DeliveryDetailsViewModel GetDeliveryDetails(string username, string id);

        AllUserDeliveriesViewModel AllUserDeliveries(string username);
    }
}
