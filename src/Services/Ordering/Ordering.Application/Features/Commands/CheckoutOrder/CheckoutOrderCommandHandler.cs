using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;

using Ordering.Domain.Entities;
using Ordering.Application.Models;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.Order);

            var newOrder = await _orderRepository.AddAsync(order);

            if (newOrder is null)
                _logger.LogError("There was an error creating the order");

            _logger.LogInformation("Order is succesfully created");

            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email()
            {
                To = "pedrottsantos@gmail.com",
                Body = $"{order.UserName} has just made an order. Price: ${order.TotalPrice}"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was an error sending this Email. {ex.Message}");
            }
        }
    }
}
