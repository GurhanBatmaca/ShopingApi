using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webapi.DTO;

namespace shopapp.webapi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController: ControllerBase
    {
        protected readonly IProductService productService;
        protected readonly ICategoryService categoryService;

        public ProductsController(IProductService _productService,ICategoryService _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;
        }

        [HttpGet]
        [Route("page={page}")]
        public async Task<IActionResult> GetAllProducts(int page=1)
        {
            var pageSize = 2;
            var products = await productService.GetAllProductsByPage(page,pageSize);

            return Ok(products);
        }

        // [Authorize(Roles ="Admin")]

        [HttpGet]
        [Route("id={id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetByIdAsync(id);

            return Ok(product);
        }

        // // [Authorize(Roles ="Admin,Customer")]

        [HttpGet]
        [Route("url={url}")]
        public async Task<IActionResult> GetProductDetails(string url)
        {
            var entity = await productService.GetProductDetails(url);
            var product = EntityToDTO.ProductToDTO(entity!);

            return Ok(product);
        }

    }
}