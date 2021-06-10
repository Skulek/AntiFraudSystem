using System;
using System.Threading.Tasks;

namespace AntiFraud.Orders.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, OrderState state)
        {
            await Task.Yield();  
            Console.WriteLine($"Sending Email to : {email}  Body : Your Order was {nameof(state)}. {(state == OrderState.Denied ? "Contact Support" : "")} {(state == OrderState.Confirmed ? "Wait for your cargo" : "")} ");
        }
    }
}
