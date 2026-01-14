

namespace Business.Services.Abstract
{
    public interface IProductService
    {
        public Task<List<GetAllProductsDto>> GetAllProductsAsync();
        public Task<GetProductDto> GetProductById(Guid id);
        public Task AddProduct(CreateProductDto dto);
        public Task DeleteProductById(Guid id);
    }
}
