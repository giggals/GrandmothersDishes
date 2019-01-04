using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;
using System.Collections.Generic;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.EmployeeService
{
    public interface IEmployeeService
    {
        void CreateEmployee(CreateEmployeeViewModel employeeModel);

        AllEmployeesViewModel GetAllEmployees();

        void DismissEmployee(string id); 
    }
}
