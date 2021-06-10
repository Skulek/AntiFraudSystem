using System;
using System.Collections.Generic;
using AntiFraud.Products.Entities;

namespace AntiFraud.Orders.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public Address Address { get; set; }
        public List<Product> Products { get; set; }
        public OrderState State { get; set; }
    }
}
