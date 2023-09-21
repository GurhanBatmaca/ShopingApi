using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IProductService: IValidator<Product>
    {
        Task<bool> CreateAsync(Product entity);
        Task<List<Product>?> GetAllProductsByPage(int page,int pageSize);
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetProductDetails(string url);
    }
}