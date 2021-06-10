using System;
using System.Collections.Generic;
using System.Linq;
using AntiFraud.Orders;
using AntiFraud.Products.Models;
using AutoMapper;

namespace AntiFraud.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper mapper;
        private readonly AntiFraudDbContext orderDbContext;


        public ProductRepository(IMapper mapper, AntiFraudDbContext orderDbContext)
        {
            this.mapper = mapper;
            this.orderDbContext = orderDbContext;
        }


        public IEnumerable<Product> GetProducts()
        {
            var products = orderDbContext.Products.AsEnumerable();
            return mapper.Map<IEnumerable<Product>>(products);
        }
    }
}
