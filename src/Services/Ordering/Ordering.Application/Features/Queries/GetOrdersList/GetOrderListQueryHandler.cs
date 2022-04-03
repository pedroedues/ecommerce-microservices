using MediatR;

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ordering.Application.Contracts.Persistence;
using AutoMapper;
using System;

namespace Ordering.Application.Features.Queries.GetOrdersList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IEnumerable<OrderViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
           var orders = await _orderRepository.GetByUserName(request.UserName, cancellationToken);

            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }
    }
}
