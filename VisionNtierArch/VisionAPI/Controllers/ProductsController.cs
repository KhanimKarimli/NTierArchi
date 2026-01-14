using Business.Services.Abstract;
using Entities.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _service.GetAllProductsAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok(await _service.GetProductById(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto dto)
        {
            await _service.AddProduct(dto);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            _service.DeleteProductById(id);
            return Ok();
        }
    }
}
