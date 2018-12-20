using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees
{
    public class AllEmployeesViewModel
    {
        public ICollection<EmployeeViewModel> Employees { get; set; }
    }
}
