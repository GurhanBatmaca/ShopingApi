using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository: EfCoreGenericRepository<Category>,ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context):base(context)
        {
            
        }

        private ShopContext? ShopContext
        { 
            get{ return context as ShopContext;} 
        }
    }
}