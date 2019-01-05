using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IRepository<Employee> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        
        private readonly IRepository<Employee> repository;
        private readonly IMapper mapper;

        public void CreateEmployee(CreateEmployeeViewModel employeeModel)
        {
            var employee = this.mapper.Map<Employee>(employeeModel);
            
            this.repository.AddAsync(employee);
            this.repository.SaveChanges();

        }

        public AllEmployeesViewModel GetAllEmployees()
        {
            var employees = this.repository.All()
                .To<EmployeeViewModel>()
                .ToList();

            var model = new AllEmployeesViewModel(){Employees = employees};

            return model;
        }

        public void DismissEmployee(string id)
        {
            var employeeToDismiss = this.repository.All()
                .FirstOrDefault(e => e.Id == id);
            

            this.repository.Delete(employeeToDismiss);
            this.repository.SaveChanges();
        }
    }
}
