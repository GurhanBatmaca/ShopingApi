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

        public void Delete(Category entity)
        {
            unitOfWork.Categories.Delete(entity);
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

        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IValidator<Category>.Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Validation(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync()
        {
            return await unitOfWork.Categories.CountAsync();
        }

        Task<Category?> ICategoryService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Category>> ICategoryService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task ICategoryService.CreateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        void ICategoryService.Update(Category entity)
        {
            throw new NotImplementedException();
        }

        void ICategoryService.Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        Task<int> ICategoryService.CountAsync()
        {
            throw new NotImplementedException();
        }

        bool IValidator<Category>.Validation(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}