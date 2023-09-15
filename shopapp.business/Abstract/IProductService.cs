using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IProductService: IValidator<Product>
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task CreateAsync(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

        Task<List<Product>?> GetHomePageProducts();
        Task<List<Product>?> GetProductsByCategory(string category,int page,int pageSize);
        Task<int> GetProductsCountByCategory(string category);
        Task<Product?> GetProductDetails(string url);
        Task<List<Product>?> GetSearchResult(string q, int page, int pageSize);
        Task<int> GetProductsCountBySearch(string searchString);
        Task<List<Product>?> GetPopularProducts(int page,int pageSize);
        Task<int> GetProductsCountByPopular();
        Task<int> CountAsync();
        Task<List<Product>?> GetAllProductsByPage(int page,int pageSize);
        Task<Product?> GetByIdWithCategories(int id);
        void Update(Product entity,int[] categoriesIds);

    }
}