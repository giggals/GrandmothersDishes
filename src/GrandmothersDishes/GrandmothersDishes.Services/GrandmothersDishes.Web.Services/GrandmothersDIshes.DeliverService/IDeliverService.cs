using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDIshes.DeliverService
{
    public interface IDeliverService
    {
        void Deliver(DeliverViewModel deliverModel, string username);
    }
}
