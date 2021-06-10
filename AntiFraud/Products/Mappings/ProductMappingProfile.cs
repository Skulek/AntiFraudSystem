using AntiFraud.Orders.Dtos;
using AntiFraud.Products.Models;
using AutoMapper;
namespace AntiFraud.Products.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            this.CreateMap<Product, ProductDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            this.CreateMap<Product, Entities.Product>().ReverseMap();
        }
    }
}
