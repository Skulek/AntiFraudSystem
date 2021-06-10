using System.Collections.Generic;
using AntiFraud.Orders.Dtos;

namespace AntiFraud.Orders.Services
{
    public interface IOrderService
    {
        bool PlaceOrder(OrderDto order);
        IEnumerable<OrderDto> GetOrders();
    }
}