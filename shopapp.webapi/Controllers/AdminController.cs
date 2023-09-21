using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webapi.Helpers;
using shopapp.webapi.Model;

namespace shopapp.webapi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController: ControllerBase
    {
        protected readonly IProductService productService;
        public AdminController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpPost]
        [Route("createproduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var entity = ModelToEntity.ModelToProduct(model);

            if(await productService.CreateAsync(entity))
            {
                return Ok(model);
            }

            return BadRequest( new ResponseObject{ Message = productService.Message } );
        }
    }
}