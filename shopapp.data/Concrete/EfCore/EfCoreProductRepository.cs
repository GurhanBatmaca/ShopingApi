using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreProductRepository: EfCoreGenericRepository<Product>,IProductRepository
    {
        public EfCoreProductRepository(ShopContext context): base(context)
        {           
        }

        private ShopContext? ShopContext
        { 
            get {return context as ShopContext;}  
        }

        public async Task<List<Product>?> GetAllProductsByPage(int page, int pageSize)
        {
            return await ShopContext!.Products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategories(int id)
        {
            return await ShopContext!.Products
                                    .Where(p => p.Id == id)
                                    .Include(p => p.ProductCategories!)
                                    .ThenInclude(p => p.Category)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<Product>?> GetHomePageProducts()
        {            
            return await ShopContext!.Products.Where(i=>i.IsHome && i.IsAproved).ToListAsync();
        }

        public async Task<List<Product>?> GetPopularProducts(int page, int pageSize)
        {
            var products = ShopContext!.Products
                                        .Where(i => i.IsPopular && i.IsAproved)
                                        .AsQueryable();

            return await products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();

        }

        public async Task<Product?> GetProductDetails(string url)
        {
            return await ShopContext!.Products
                                    .Where(i=>i.Url == url)
                                    .Include(e=>e.ProductCategories!)
                                    .ThenInclude(p=>p.Category)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<Product>?> GetProductsByCategory(string category, int page, int pageSize)
        {
            var products = ShopContext!.Products
                                    .Where(i=> i.IsAproved)
                                    .AsQueryable();
            
            if(!string.IsNullOrEmpty(category))
            {
                products = products
                            .Include(i => i.ProductCategories!)
                            .ThenInclude(e=> e.Category)
                            .Where(i=>i.ProductCategories!.Any(e=>e.Category!.Url==category));
            };

            return await products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetProductsCountByCategory(string category)
        {
            var products = ShopContext!.Products
                                    .Where(i=> i.IsAproved)
                                    .AsQueryable();

            if(!string.IsNullOrEmpty(category)) 
            {
                products = products
                                .Include(i => i.ProductCategories!)
                                .ThenInclude(e=> e.Category)
                                .Where(i=>i.ProductCategories!.Any(e=>e.Category!.Url==category));
            };

            return await products.CountAsync();
        }

        public async Task<int> GetProductsCountByPopular()
        {
            return await ShopContext!.Products
                                        .Where(i=> i.IsAproved && i.IsPopular)
                                        .CountAsync();

        }

        public async Task<int> GetProductsCountBySearch(string searchString)
        {
            var products = ShopContext!.Products
                                    .Where(i=> i.IsAproved)
                                    .AsQueryable();

            if(!string.IsNullOrEmpty(searchString)) 
            {
                products = products
                                .Where(i=>i.IsAproved && (i.Name!.ToLower().Contains(searchString.ToLower()) || i.Description!.ToLower().Contains(searchString.ToLower())))
                                .AsQueryable();

            };

            return await products.CountAsync();
        }

        public async Task<List<Product>?> GetSearchResult(string q, int page, int pageSize)
        {
            var products = ShopContext!.Products
                                                .Where(i=>i.IsAproved && (i.Name!.ToLower().Contains(q.ToLower()) || i.Description!.ToLower().Contains(q.ToLower())))
                                                .AsQueryable();

            return await products.Skip((page-1)* pageSize).Take(pageSize).ToListAsync();
        }

        public void Update(Product entity, int[] categoriesIds)
        {
            var product = ShopContext!.Products
                                .Include(i =>i.ProductCategories)
                                .FirstOrDefault(i=>i.Id == entity.Id);

            if(product!=null)
            {
                product.Name=entity.Name;
                product.Price=entity.Price;
                product.Description=entity.Description;
                product.Url=entity.Url;
                product.ImageUrl=entity.ImageUrl;
                product.IsAproved=entity.IsAproved;
                product.IsHome=entity.IsHome;
                product.IsPopular = entity.IsPopular;

                product.ProductCategories = categoriesIds.Select(catId => new ProductCategory()
                {
                    ProductId = entity.Id,
                    CategoryId = catId
                }).ToList();

                ShopContext.SaveChanges();

            }
        }

    }
}