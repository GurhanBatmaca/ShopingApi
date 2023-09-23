namespace shopapp.data.Abstract
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
    }
}