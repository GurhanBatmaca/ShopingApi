using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPatch]
        [Route("updateproduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            
            var exEntity = await productService.GetByIdAsync(id);

            if(exEntity == null)
            {
                return BadRequest( new ResponseObject{ Message = "Entity not found" } );
            }

            var product = new Product()
            {
                Id = exEntity.Id,
                Name = exEntity!.Name,
                Price = exEntity.Price,
                Description = exEntity.Description,
                IsAproved = exEntity.IsAproved,
                IsHome = exEntity.IsHome,
                IsPopular = exEntity.IsPopular,
                Url = exEntity.Url
            };

            patchDocument.ApplyTo(product,ModelState);

            if(await productService.UpdateAsync(exEntity,product))
            {

                return Ok(product);

            }

            return BadRequest( new ResponseObject{ Message = productService.Message } );

        }
    
        [HttpDelete]
        [Route("deleteproduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null)
            {
                return BadRequest( new ResponseObject{ Message = "Id is required." } );
            }

            var entity = await productService.GetByIdAsync((int)id);

            if(entity == null)
            {
                return BadRequest( new ResponseObject{ Message = "Product not found." } );
            }

            await productService.DeleteAsync(entity);

            return Ok( new ResponseObject{ Message = "Delete product successful." } );
        }


    }
}