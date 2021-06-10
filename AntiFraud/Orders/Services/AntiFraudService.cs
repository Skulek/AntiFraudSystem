using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiFraud.Orders.Factories;
using AntiFraud.Orders.Models;
using AntiFraud.Orders.Repository;

namespace AntiFraud.Orders.Services
{
    public class AntiFraudService : IAntiFraudService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IEmailService emailService;
        private readonly List<IAntiFraudPolicy> policies;
        private readonly double FractorOfAverageAmount;

        public AntiFraudService(IOrderRepository orderRepository, IAntiFraudPolicyFactory antiFraudPolicyFactory, IEmailService emailService)
        {
            this.orderRepository = orderRepository;
            policies = antiFraudPolicyFactory.GetAntiFraudPolicy();
            FractorOfAverageAmount = antiFraudPolicyFactory.GetFractorOfAverageAmmount();
            this.emailService = emailService;
        }

        public async ValueTask ValidateOrders()
        {
           var orders = orderRepository.GetOrders().
                Where(order => order.State == OrderState.Placed);
           if (orders.Any())
           {
                return;
           }
           double averageAmmount = orderRepository.GetOrders().Sum(x => x.Amount) * FractorOfAverageAmount;
           foreach(var order in orders)
           {
                var validationorder = this.ValidateOrder(order, averageAmmount);
                orderRepository.UpdateOrderState(order.Id, validationorder);
                await emailService.SendEmailAsync(order.Email, validationorder);
           }
        }

        public OrderState ValidateOrder(Models.Order order, double averageAmmount)
        {
            foreach (var policy in policies)
            {
                if (((policy.IsNewUser && order.IsNewUser) || !policy.IsNewUser)
                    && policy.MaximumAmount < order.Amount
                    && policy.DissalowedCountry.Equals(order.Address.Country.ToUpper()))
                {
                    return OrderState.Denied;
                }

                if(averageAmmount < order.Amount)
                {
                    return OrderState.Denied;
                }
            }
            

            return OrderState.Confirmed;
        }
    }
}
