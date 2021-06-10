using System;
using AntiFraud.Orders.Dtos;
using AntiFraud.Orders.Models;
using AutoMapper;
namespace AntiFraud.Orders.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            this.CreateMap<Address, AddressDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            this.CreateMap<Order, OrderDto>().ReverseMap()
                .ForMember(dest=>dest.Id, opt => opt.Ignore())
                .ForMember(dest=> dest.State, opt => opt.Ignore());
            

            this.CreateMap<Order, Entities.Order>().ReverseMap();
            this.CreateMap<Address, Entities.Address>().ReverseMap();

        }
    }
}
