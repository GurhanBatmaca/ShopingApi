using Microsoft.AspNetCore.JsonPatch;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>?> GetHomePageProducts();
        Task<List<Product>?> GetProductsByCategory(string category,int page,int pageSize);
        Task<Product?> GetProductDetails(string url);
        Task<List<Product>?> GetSearchResult(string q,int page,int pageSize);
        Task<List<Product>?> GetPopularProducts(int page,int pageSize);
        Task<List<Product>?> GetAllProductsByPage(int page,int pageSize);
        Task UpdateAsync(Product exEntity,Product product);

    }
}