using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AntiFraud.Orders.Dtos;
using AntiFraud.Orders.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AntiFraud.Orders.Extensions
{
    public class MigratorHostedService : IHostedService
    {
        private readonly IOrderService orderService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MigratorHostedService(IOrderService orderService, IWebHostEnvironment webHostEnviroment)
        {
            this.orderService = orderService;
            this.webHostEnvironment = webHostEnviroment;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var path = Path.Combine(webHostEnvironment.ContentRootPath, "Orders.json");
                var jsonFile = System.IO.File.ReadAllText(path);
                var orders = JsonConvert.DeserializeObject<List<OrderDto>>(jsonFile);
                foreach (var order in orders)
                {
                    orderService.PlaceOrder(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
