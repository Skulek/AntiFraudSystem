using System.Collections.Generic;

namespace AntiFraud.Orders.Dtos
{
    public class OrderDto
    {
            public string Email { get; set; }
            public long Amount { get; set; }
            public string Currency { get; set; }
            public AddressDto Address { get; set; }
            public List<ProductDto> Products { get; set; }
    }
}
