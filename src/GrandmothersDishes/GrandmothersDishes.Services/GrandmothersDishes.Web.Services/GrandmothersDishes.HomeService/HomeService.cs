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
            IMapper mapper
          )
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        private IRepository<Dish> repository;
        private readonly IMapper mapper;

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

       
    }
}
