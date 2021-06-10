using System.Collections.Generic;
using System.Linq;
using AntiFraud.Orders.Models;
using AutoMapper;

namespace AntiFraud.Orders.Repository
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly IMapper mapper;
        private readonly AntiFraudDbContext orderDbContext;
        public OrderRepository(IMapper mapper, AntiFraudDbContext orderDbContext)
        {
            this.mapper = mapper;
            this.orderDbContext = orderDbContext;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orderEntities = orderDbContext.Orders.AsEnumerable();
            var orderModels =  mapper.Map<IEnumerable<Order>>(orderEntities);
            foreach (var order in orderModels)
            {
                if (orderModels.Any(or => or.Id != order.Id && or.Email.Equals(order.Email)))
                {
                    order.IsNewUser = false;
                }
            }
            return orderModels;
        }

        public bool PlaceOrder(Order order)
        {
            var entitiesModel = mapper.Map<Entities.Order>(order);
            orderDbContext.Orders.Add(entitiesModel);
            return orderDbContext.SaveChanges() == 1;
        }

        public void UpdateOrderState(int id, OrderState orderState)
        {
           var dbOrder = orderDbContext.Orders.Find(id);
           dbOrder.State = orderState;
           orderDbContext.Update(dbOrder);
        }
    }
}
