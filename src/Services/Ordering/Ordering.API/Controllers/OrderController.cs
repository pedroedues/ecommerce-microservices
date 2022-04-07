using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using MediatR;
using Microsoft.AspNetCore.Mvc;

using Ordering.Application.Features.Commands.DeleteOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrdersList;
using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController  : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrderListQuery(userName);

            var orders = await _mediator.Send(query);

            if (orders == null)
                return NoContent();

            return Ok(orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> CheckoutOrder(
            [FromBody] CheckoutUpdateOrderViewModel order)
        {
            var result = await _mediator.Send(new CheckoutOrderCommand(order));//Diff from course class, 100

            return CreatedAtAction(nameof(CheckoutOrder), result);
        }

        [HttpPut("{id}", Name = "UpdateOrder")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> UpdateOrder(
            [FromRoute] int id,
            [FromBody] CheckoutUpdateOrderViewModel order)
        {
            var result = await _mediator.Send(new UpdateOrderCommand(id, order));

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> DeleteOrder(
            [FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));

            return NoContent();
        }
    }
}
