using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetByUserName(string userName, CancellationToken cancellation);
    }
}
