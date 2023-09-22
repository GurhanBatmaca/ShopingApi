using Microsoft.AspNetCore.JsonPatch;
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

        public string? Message { get; set; }

        public async Task<bool> CreateAsync(Product entity)
        {
            if(!Validation(entity))
            {
                return false;
            }

            await unitOfWork.Products.CreateAsync(entity);
            Message += "Product created";
            return true;
        }

        public bool Validation(Product entity)
        {
            if(string.IsNullOrEmpty(entity.Name))
            {
                Message += "Name is required";
                return false;
            }
            if(string.IsNullOrEmpty(entity.Description))
            {
                Message += "Description is required";
                return false;
            }
            if(entity.Price < 1)
            {
                Message += "Price is must be positive number.";
                return false;
            }
            
            return true;
        }

        public async Task<List<Product>?> GetAllProductsByPage(int page, int pageSize)
        {
            return await unitOfWork.Products.GetAllProductsByPage(page, pageSize);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Product?> GetProductDetails(string url)
        {
            return await unitOfWork.Products.GetProductDetails(url);
        }

        public async Task<bool> UpdateProduct(int id, JsonPatchDocument<Product> patchDocument)
        {
            var entity = await unitOfWork.Products.GetByIdAsync(id);

            if(entity == null)
            {
                Message += "Product not faound.";
                return false;
            }

            await unitOfWork.Products.UpdateProduct(id,patchDocument);
            Message += "Product updated.";
            return true;
        }
    }
}