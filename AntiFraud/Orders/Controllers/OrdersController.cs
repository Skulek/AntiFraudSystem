using System;
using System.Collections.Generic;
using System.Linq;
using AntiFraud.Orders.Dtos;
using AntiFraud.Orders.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AntiFraud.Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<OrderDto> Get()
        {
            return orderService.GetOrders().ToList();
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderDto order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           var status = orderService.PlaceOrder(order);
           return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
