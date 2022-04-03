using AutoMapper;

using Ordering.Domain.Entities;
using Ordering.Application.Features.Queries.GetOrdersList;
using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();

            CreateMap<Order, CheckoutUpdateOrderViewModel>().ReverseMap();
        }

    }
}
