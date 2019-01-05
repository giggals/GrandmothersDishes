using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;
using Microsoft.AspNetCore.Mvc;


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

        public async Task<Delivery> Deliver(DeliverViewModel deliverModel , string username)
        {
            var deliver = this.mapper.Map<Delivery>(deliverModel);

            deliver.DeliveredOn = DateTime.UtcNow.Date;

            Random rnd = new Random();
            int minuteToDeliver = rnd.Next(20, 58);

            deliver.TimeToDeliver = minuteToDeliver;

            var user = this.usersRepository.All().FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return null;
            }

            deliver.User = user;
            
            var orders = this.orderService.GetAllOrders(username);

            foreach (var order in orders)
            {
                order.Status = Status.Completed;
            }
            
           await this.repository.AddAsync(deliver);
            this.repository.SaveChanges();

            return deliver;
        }

        public DeliveryDetailsViewModel GetDeliveryDetails(string username , string deliveryId)
        {
            Random rnd = new Random();

            var user = this.usersRepository.All().FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return null;
            }
            
            var deliver = this.repository.All()
                .Select(x => new DeliveryDetailsViewModel()
                {
                    Id =  x.Id,
                    Address = x.Address,
                    DeliveryType = x.DeliveryType.ToString(),
                    TimeToDeliver = x.TimeToDeliver,
                    User = user.UserName,
                    DeliveredOn = x.DeliveredOn.ToShortDateString(),
                })
                .FirstOrDefault(x => x.Id == deliveryId);

            return deliver;
        }

        public AllUserDeliveriesViewModel AllUserDeliveries(string username)
        {
            var user = usersRepository.All().FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return null;
            }

            var deliveries = this.repository.All()
                .Where(x => x.User.UserName == username)
                .To<DeliveryAllViewModel>()
                .ToList();

            var model = new AllUserDeliveriesViewModel() {Deliveries = deliveries};

            return model;
        }
    }
}
