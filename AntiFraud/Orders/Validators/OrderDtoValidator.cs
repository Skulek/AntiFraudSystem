using System;
using AntiFraud.Orders.Dtos;
using FluentValidation;

namespace AntiFraud.Orders.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(order => order.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(order => order.Address.Country).NotEmpty();
            RuleFor(order => order.Amount).GreaterThan(0);
        }
    }
}
