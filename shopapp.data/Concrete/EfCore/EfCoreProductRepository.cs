using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;
using shopapp.shared;

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

        public async Task<List<Product>?> GetSearchResult(string q, int page, int pageSize)
        {
            var products = ShopContext!.Products
                                                .Where(i=>i.IsAproved && (i.Name!.ToLower().Contains(q.ToLower()) || i.Description!.ToLower().Contains(q.ToLower())))
                                                .AsQueryable();

            return await products.Skip((page-1)* pageSize).Take(pageSize).ToListAsync();
        }

        public async Task UpdateAsync(Product exEntity,Product product)
        {
            exEntity.Id = product.Id;
            exEntity.Name = product.Name;
            exEntity.Price = product.Price;
            exEntity.Description = product.Description;
            exEntity.IsAproved = product.IsAproved;
            exEntity.IsHome = product.IsHome;
            exEntity.Url = UrlModifier.Modifie(product.Name!);

            await ShopContext!.SaveChangesAsync();
        }
    }
}