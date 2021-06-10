using System;
using System.Threading.Tasks;
using AntiFraud.Orders.Services;
using Quartz;

namespace AntiFraud.Orders
{
    [DisallowConcurrentExecution]
    public class AntiFraudJob : IJob
    {
        private readonly IAntiFraudService antiFraudService;

        public AntiFraudJob(IAntiFraudService antiFraudService)
        {
            this.antiFraudService = antiFraudService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            antiFraudService.ValidateOrders();
            return Task.CompletedTask;
        }
    }
}
