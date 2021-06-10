using System.Collections.Generic;
using AntiFraud.Orders.Dtos;

namespace AntiFraud.Products.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
    }
}