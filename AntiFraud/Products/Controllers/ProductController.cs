using System.Collections.Generic;
using System.Linq;
using AntiFraud.Orders.Dtos;
using AntiFraud.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace AntiFraud.Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService orderService;

        public ProductController(IProductService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            return orderService.GetProducts().ToList();
        }
    }
}
