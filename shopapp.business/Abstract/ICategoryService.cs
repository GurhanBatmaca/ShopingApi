using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICategoryService: IValidator<Category>
    {
        Task<Category?> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task CreateAsync(Category entity);
        void Update(Category entity);
        Task DeleteAsync(Category product);
        Task<int> CountAsync();
    }
}