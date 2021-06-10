using System;
using System.Collections.Generic;
using AntiFraud.Products.Models;

namespace AntiFraud.Orders.Models
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
        public bool IsNewUser { get; set; } = true;
    }
}

public enum OrderState
{
    Placed = 0,
    Confirmed = 1,
    Denied = 2,
}