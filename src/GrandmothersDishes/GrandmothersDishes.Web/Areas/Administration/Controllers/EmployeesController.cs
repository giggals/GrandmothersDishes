using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.EmployeeService;
using GrandmothersDishes.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Areas.Administration.Controllers
{
    public class EmployeesController : AdministrationBaseController
    {
        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        private readonly IEmployeeService employeeService;

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return this.View(); 
        }
        
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(CreateEmployeeViewModel employeeModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(employeeModel);
            }

            this.employeeService.CreateEmployee(employeeModel);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            var model = this.employeeService.GetAllEmployees();

            return this.View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Dismiss(string id)
        {
            this.employeeService.DismissEmployee(id);

            return this.RedirectToAction("All");
        }
    }
}
