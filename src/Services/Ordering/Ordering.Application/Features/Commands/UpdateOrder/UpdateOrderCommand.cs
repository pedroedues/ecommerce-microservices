using MediatR;

using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.Application.Features.Commands.UpdateOrder
{
    public class UpdateOrderCommand  : IRequest
    {
        public int Id { get; private set; }

        public CheckoutUpdateOrderViewModel Order { get; private set; }
    }
}
