using System.Threading.Tasks;

namespace AntiFraud.Orders.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, OrderState state);
    }
}