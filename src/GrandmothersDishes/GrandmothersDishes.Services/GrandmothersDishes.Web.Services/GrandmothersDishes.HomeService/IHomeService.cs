using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.HomeService
{
    public interface IHomeService
    {
        IEnumerable<HomeProductViewModel> HomeProducts();

        HomeAllProducts AllProducts();

        AllHomeEmployeesViewModel AllEmployees();

    }
}
