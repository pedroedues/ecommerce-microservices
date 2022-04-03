using System;
using System.Collections.Generic;

using MediatR;

namespace Ordering.Application.Features.Queries.GetOrdersList
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public string UserName { get; private set; }
        public GetOrderListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
