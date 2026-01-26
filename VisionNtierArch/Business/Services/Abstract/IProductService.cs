

using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Business.Services.Abstract
{
    public interface IProductService
    {
        public Task<IDataResult<List<GetAllProductsDto>>> GetAllProductsAsync();
        public Task<IDataResult<GetProductDto>> GetProductById(Guid id);
        public Task<IResult> AddProduct(CreateProductDto dto);
        public Task<IResult> DeleteProductById(Guid id);
        public Task<IResult> UpdateProduct(Guid id,UpdateProductDto dto);
    }
}
