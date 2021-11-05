using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}
