using System;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;

using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var basket = await _redisCache.GetStringAsync(userName, cancellationToken);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket), cancellationToken);

            return await GetBasket(basket.UserName, cancellationToken);
        }

        public async Task DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            await _redisCache.RemoveAsync(userName, cancellationToken);
        }
    }
}
