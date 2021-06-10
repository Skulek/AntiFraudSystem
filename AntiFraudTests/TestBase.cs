using AntiFraud.Orders.Dtos;
using AntiFraud.Orders.Models;
using AntiFraud.Products.Models;

namespace AntiFraudTests
{
    public class TestBase
    {
        public static OrderDto CreateRandomOrderDto()
        {
            return new OrderDto
            {
                Amount = Faker.RandomNumber.Next(0, 10000),
                Address = new AddressDto
                {
                    City = Faker.Address.City(),
                    Country = Faker.Address.Country(),
                    Street = Faker.Address.StreetName(),
                    Zipcode = Faker.Address.ZipCode(),
                },
                Currency = Faker.Currency.Name(),
                Email = Faker.Internet.Email(),
                Products = new System.Collections.Generic.List<ProductDto>
                    {
                        new ProductDto { Name = Faker.Lorem.GetFirstWord(), Quantity = Faker.RandomNumber.Next(1, 10)},
                        new ProductDto { Name = Faker.Lorem.GetFirstWord(), Quantity = Faker.RandomNumber.Next(1, 10)}
                    }
            };
        }

        public static Order CreateRandomOrderModel()
        {
            return new Order
            {
                Amount = Faker.RandomNumber.Next(0, 10000),
                Address = new Address
                {
                    City = Faker.Address.City(),
                    Country = Faker.Address.Country(),
                    Street = Faker.Address.StreetName(),
                    Zipcode = Faker.Address.ZipCode(),
                },
                Currency = Faker.Currency.Name(),
                Email = Faker.Internet.Email(),
                Products = new System.Collections.Generic.List<Product>
                    {
                        new Product { Name = Faker.Lorem.GetFirstWord(), Quantity = Faker.RandomNumber.Next(1, 10)},
                        new Product { Name = Faker.Lorem.GetFirstWord(), Quantity = Faker.RandomNumber.Next(1, 10)}
                    }
            };
        }
    }
}