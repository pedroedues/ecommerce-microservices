using System.Threading;
using System.Threading.Tasks;

using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);

        Task<ShoppingCart> UpdateBasket(ShoppingCart basket, CancellationToken cancellationToken = default);

        Task DeleteBasket(string userName, CancellationToken cancellationToken = default);
    }
}
