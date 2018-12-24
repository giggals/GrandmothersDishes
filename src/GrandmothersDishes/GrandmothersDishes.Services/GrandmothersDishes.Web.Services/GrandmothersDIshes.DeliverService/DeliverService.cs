using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDIshes.DeliverService
{
    public class DeliverService : IDeliverService
    {
        public DeliverService(IRepository<Delivery> repository,
         IOrderService orderService,
            IRepository<GrandMothersUser> usersRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.orderService = orderService;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        private readonly IRepository<Delivery> repository;
        private readonly IOrderService orderService;
        private readonly IRepository<GrandMothersUser> usersRepository;
        private readonly IMapper mapper;

        public void Deliver(DeliverViewModel deliverModel , string username)
        {
            var deliver = this.mapper.Map<Delivery>(deliverModel);

            var user = this.usersRepository.All().FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return;
            }

            deliver.User = user;
            
            var orders = this.orderService.GetAllOrders(username);

            foreach (var order in orders)
            {
                order.Status = Status.Completed;
            }

            this.repository.AddAsync(deliver);
            this.repository.SaveChanges();
        }
    }
}
