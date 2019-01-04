using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDIshes.DeliverService
{
    public interface IDeliverService
    {
        Task<Delivery> Deliver(DeliverViewModel deliverModel, string username);

        DeliveryDetailsViewModel GetDeliveryDetails(string username, string id);

        AllUserDeliveriesViewModel AllUserDeliveries(string username);
    }
}
