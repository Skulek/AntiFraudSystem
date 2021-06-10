using System.Collections.Generic;
using AntiFraud.Orders.Factories;
using AntiFraud.Orders.Repository;
using AntiFraud.Orders.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AntiFraudTests
{
public class AntiFraudServiceTests : TestBase
{
    private IAntiFraudService fraudService;
    private readonly IOrderRepository orderRepository;
    private readonly IAntiFraudPolicyFactory antiFraudPolicyFactory;
    private readonly IEmailService emailService;

    public AntiFraudServiceTests()
    {
        orderRepository = Substitute.For<IOrderRepository>();
        antiFraudPolicyFactory = Substitute.For<IAntiFraudPolicyFactory>();
        emailService = Substitute.For<IEmailService>();
    }

    [Theory]
    [InlineData ("Benin", 50, 100 ,OrderState.Confirmed)]
    [InlineData ("Poland", 101, 200, OrderState.Confirmed)]
    [InlineData ("Nigeria", 200, 100, OrderState.Denied)]
    [InlineData ("Belize", 10000, 100, OrderState.Denied)]
    public void WHEN_order_has_invalid_values_THEN_order_state_denied(string country, long amount, double averageAmount, OrderState orderState)
    {
            antiFraudPolicyFactory.GetAntiFraudPolicy().Returns(
            new List<AntiFraud.Orders.Models.IAntiFraudPolicy>
            {
                new AntiFraud.Orders.Models.AntiFraudPolicy
                { IsNewUser = false, DissalowedCountry="NIGERIA", MaximumAmount=100 }
            });

            fraudService = new AntiFraudService(orderRepository, antiFraudPolicyFactory, emailService);

            var order = CreateRandomOrderModel();
            order.Amount = amount;
            order.Address.Country = country;
            var validationState = fraudService.ValidateOrder(order, averageAmount);
            validationState.Should().Be(orderState);
    }
}
}
