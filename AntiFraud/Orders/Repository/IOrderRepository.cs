using System.Collections.Generic;
using AntiFraud.Orders.Models;

namespace AntiFraud.Orders.Repository
{
    public interface IOrderRepository
    {
        bool PlaceOrder(Order order);
        IEnumerable<Order> GetOrders();
        void UpdateOrderState(int id, OrderState orderState);
    }
}