using System;
using Xunit;
using AntiFraud.Orders.Services;
using AntiFraud.Orders.Controllers;
using NSubstitute;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using AntiFraud.Orders.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AntiFraudTests
{
    public class OrderControllerTests: TestBase
    {
        private readonly IOrderService orderService;
        private readonly OrdersController ordersController;
        public OrderControllerTests()
        {
            orderService = Substitute.For<IOrderService>();
            ordersController = new OrdersController(Substitute.For<ILogger<OrdersController>>(), orderService);
        }


        [Theory]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(0)]
        public void WHEN_user_ask_for_orders_THEN_should_return_number_of_orders(int numberOfItems)
        {
            //arange
            OrderDto[] orders = new OrderDto[numberOfItems];
            for (int i = 0; i < numberOfItems; i++)
            {
                OrderDto order = CreateRandomOrderDto();

                orders[i] = order;
            }
            orderService.GetOrders().Returns(orders);
            
            ordersController.Get().Should().HaveCount(numberOfItems);
        }

        

        [Fact]
        public void WHEN_user_places_order_THEN_should_return_sucess()
        {
            OrderDto orderDto = CreateRandomOrderDto();
            orderService.PlaceOrder(orderDto).Returns(true);
            var res = ordersController.PlaceOrder(orderDto);
            res.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void WHEN_user_places_order_andsomething_went_wrong_THEN_should_return_error()
        {
            OrderDto orderDto = CreateRandomOrderDto();
            orderService.PlaceOrder(orderDto).Returns(false);
            StatusCodeResult res = (StatusCodeResult)ordersController.PlaceOrder(orderDto);
            res.StatusCode.Should().Be(500);
        }

        
    }
}
