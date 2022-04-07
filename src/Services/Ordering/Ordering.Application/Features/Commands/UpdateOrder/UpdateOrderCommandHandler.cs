using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;

using Ordering.Domain.Entities;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToUpdate is null)
                throw new NotFoundException(nameof(Order), request.Id);


            _mapper.Map(request.Order, orderToUpdate, typeof(CheckoutUpdateOrderViewModel), typeof(Order));

            await _orderRepository.UpdateAsync(request.Id, orderToUpdate);
            // this handle r followed an different logic. Maybe the mapping is gonna have problems beacause VM has not Id and Order has.This point was at class 96

            _logger.LogInformation($"Order {request.Id} was succesfully updated");

            return Unit.Value;
        } 
    }
}
