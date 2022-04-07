using System;

using MediatR;

namespace Ordering.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public CheckoutUpdateOrderViewModel Order { get; private set; }

        public CheckoutOrderCommand(CheckoutUpdateOrderViewModel order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
        }
    }
}
