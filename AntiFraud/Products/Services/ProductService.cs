using System;
using System.Collections.Generic;
using AntiFraud.Orders.Dtos;
using AntiFraud.Products.Repositories;
using AutoMapper;

namespace AntiFraud.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            var products = productRepository.GetProducts();
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
