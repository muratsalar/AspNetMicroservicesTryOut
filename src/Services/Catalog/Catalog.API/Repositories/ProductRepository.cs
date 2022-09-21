using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ICatalogContext catalogContext;

        public async Task CreateProduct(Product product)
        {
            await catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteRes = await catalogContext.Products.DeleteOneAsync(filter: _ => _.Id == id);

            return deleteRes.IsAcknowledged && deleteRes.DeletedCount > 1;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await catalogContext.Products.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filters = Builders<Product>.Filter.Eq(_ => _.Category, categoryName);

            return await catalogContext.Products.Find(filters).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filters = Builders<Product>.Filter.Eq(_ => _.Name , name);

            return await catalogContext.Products.Find(filters).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await catalogContext.Products.Find( _ => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateInfo = await catalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateInfo.IsAcknowledged && updateInfo.ModifiedCount > 0;
        }
    }
}
