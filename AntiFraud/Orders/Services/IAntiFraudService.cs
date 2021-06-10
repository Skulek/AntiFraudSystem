using System.Collections.Generic;
using System.Threading.Tasks;
using AntiFraud.Orders.Models;

namespace AntiFraud.Orders.Services
{
    public interface IAntiFraudService
    { 
        ValueTask ValidateOrders();
        OrderState ValidateOrder(Order order, double averageAmmount);
    }
}
