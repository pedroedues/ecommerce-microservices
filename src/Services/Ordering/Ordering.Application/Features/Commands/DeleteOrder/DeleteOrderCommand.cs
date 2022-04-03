using MediatR;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; protected set; }

        public DeleteOrderCommand(int id)
        {
            Id = id;
        }

    }
}
