
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace VisionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service=service;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var result=await _service.GetAllProductsAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await _service.GetProductById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto dto)
        {
           var result= await _service.AddProduct(dto);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result=await _service.DeleteProductById(id);
			if(result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
        {
           var result=await _service.UpdateProduct(id, dto);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
    }
}
