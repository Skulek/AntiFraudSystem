using System.Collections.Generic;

namespace AntiFraud.Products.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Models.Product> GetProducts();
    }
}