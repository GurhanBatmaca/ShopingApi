using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;


namespace shopapp.business.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IUnitOfWork unitOfWork;
        public ProductManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        

        public async Task CreateAsync(Product entity)
        {
            await unitOfWork.Products.CreateAsync(entity);
        }

        public void Delete(Product entity)
        {
            unitOfWork.Products.Delete(entity);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await unitOfWork.Products.GetByIdAsync(id);
        }

        public void Update(Product entity)
        {
            unitOfWork.Products.Update(entity);
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Validation(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>?> GetHomePageProducts()
        {
            return await unitOfWork.Products.GetHomePageProducts();
        }

        public async Task<List<Product>?> GetProductsByCategory(string category, int page, int pageSize)
        {
            return await unitOfWork.Products.GetProductsByCategory(category, page, pageSize);
        }

        public async Task<int> GetProductsCountByCategory(string category)
        {
            return await unitOfWork.Products.GetProductsCountByCategory(category);
        }

        public async Task<Product?> GetProductDetails(string url)
        {
            return await unitOfWork.Products.GetProductDetails(url);
        }

        public async Task<List<Product>?> GetSearchResult(string q, int page, int pageSize)
        {
            return await unitOfWork.Products.GetSearchResult(q, page, pageSize);
        }

        public async Task<int> GetProductsCountBySearch(string searchString)
        {
            return await unitOfWork.Products.GetProductsCountBySearch(searchString);
        }

        public async Task<List<Product>?> GetPopularProducts(int page, int pageSize)
        {
            return await unitOfWork.Products.GetPopularProducts(page, pageSize);
        }

        public async Task<int> GetProductsCountByPopular()
        {
            return await unitOfWork.Products.GetProductsCountByPopular();
        }

        public async Task<int> CountAsync()
        {
            return await unitOfWork.Products.CountAsync();
        }

        public async Task<List<Product>?> GetAllProductsByPage(int page, int pageSize)
        {
            return await unitOfWork.Products.GetAllProductsByPage(page, pageSize);
        }

        public async Task<Product?> GetByIdWithCategories(int id)
        {
            return await unitOfWork.Products.GetByIdWithCategories(id);
        }

        public void Update(Product entity, int[] categoriesIds)
        {
            unitOfWork.Products.Update(entity, categoriesIds);
        }
    }
}