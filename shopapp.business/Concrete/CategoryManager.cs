using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;


namespace shopapp.business.Concrete
{
    public class CategoryManager : ICategoryService
    {    

        private readonly IUnitOfWork unitOfWork;
        public CategoryManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }   

        public async Task CreateAsync(Category entity)
        {
            await unitOfWork.Categories.CreateAsync(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            await unitOfWork.Categories.DeleteAsync(entity);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await unitOfWork.Categories.GetByIdAsync(id);
        }

        public void Update(Category entity)
        {
            unitOfWork.Categories.Update(entity);
        }

        public string? Message { get; set; }

        public bool Validation(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync()
        {
            return await unitOfWork.Categories.CountAsync();
        }
    }
}