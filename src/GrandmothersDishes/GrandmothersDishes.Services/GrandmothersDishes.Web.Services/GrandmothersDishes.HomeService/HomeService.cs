using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Home;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.HomeService
{
    public class HomeService : IHomeService
    {
        public HomeService(IRepository<Dish> repository,
            IRepository<Employee> employeeRepository
          )
        {
            this.repository = repository;
            this.employeeRepository = employeeRepository;
        }

        private IRepository<Dish> repository;
        private readonly IRepository<Employee> employeeRepository;

        public IEnumerable<HomeProductViewModel> HomeProducts()
        {
            var dishes = this.repository.All().To<HomeProductViewModel>()
                .ToList();
            
            return dishes;
        }

        public HomeAllProducts AllProducts()
        {
            var products = HomeProducts().ToList();

            var allProducts = new HomeAllProducts() {Products = products};

            return allProducts;
        }

        public AllHomeEmployeesViewModel AllEmoloyees()
        {
            var employees = this.employeeRepository.All()
                .To<HomeEmployeeViewModel>()
                .ToList();

            var allEmployees = new AllHomeEmployeesViewModel() {Employees = employees};

            return allEmployees;
        }
        
    }
}
