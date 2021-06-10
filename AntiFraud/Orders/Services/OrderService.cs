using System;
using System.Collections.Generic;
using AntiFraud.Orders.Dtos;
using AntiFraud.Orders.Repository;
using AutoMapper;

namespace AntiFraud.Orders.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            var orders = orderRepository.GetOrders();
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public bool PlaceOrder(OrderDto orderDto)
        {
            var order = mapper.Map<Models.Order>(orderDto);
            return orderRepository.PlaceOrder(order);
        }
    }
}
