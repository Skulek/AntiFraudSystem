using System;
using AntiFraud.Orders.Validators;
using FluentAssertions;
using Xunit;

namespace AntiFraudTests
{
    public class OrderValidatorTests : TestBase
    {
        private OrderDtoValidator orderDtoValidator;
        public OrderValidatorTests()
        {
            orderDtoValidator = new OrderDtoValidator();
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("asdasdasd", false)]
        [InlineData("asdasdasd@gmail.com", true)]
        public void WHEN_order_dont_have_email_THEN_is_invalid(string email, bool valid)
        {
            var order = CreateRandomOrderDto(); // You should create a valid address object here
            order.Email = email; // and then invalidate the specific things you want to test
            orderDtoValidator.Validate(order).IsValid.Should().Be(valid);
        }

        [Fact]
        public void WHEN_order_dont_have_country_THEN_is_invalid()
        {
            var order = CreateRandomOrderDto(); // You should create a valid address object here
            order.Address.Country = null; // and then invalidate the specific things you want to test
            orderDtoValidator.Validate(order).IsValid.Should().BeFalse();
        }

        [Fact]
        public void WHEN_order_amount_is_equal_or_below_0_THEN_is_invalid()
        {
            var order = CreateRandomOrderDto(); // You should create a valid address object here
            order.Amount = 0; // and then invalidate the specific things you want to test
            orderDtoValidator.Validate(order).IsValid.Should().BeFalse();
        }
    }
}
