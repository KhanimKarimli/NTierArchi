
namespace Business.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<List<GetAllProductsDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<List<GetAllProductsDto>>(products);
        }

		public async Task<GetProductDto> GetProductById(Guid id)
		{
			var product = await _unitOfWork.ProductRepository.GetAsync(p=>p.Id==id);
            if(product==null)
            {
                throw new NotFoundException(ExceptionMessages.ProductNotFound);
            }
            return _mapper.Map<GetProductDto>(product);
		}

		public async Task AddProduct(CreateProductDto dto)
        {
			var result =_mapper.Map<Product>(dto);
            result.CreatedAt = DateTime.UtcNow;
			await _unitOfWork.ProductRepository.AddAsync(result);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductById(Guid id)
        {
			var product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id==id);
			if (product==null)
			{
				throw new NotFoundException(ExceptionMessages.ProductNotFound);
			}
			_unitOfWork.ProductRepository.Delete(product);
			await _unitOfWork.SaveAsync();
		}


        public async Task UpdateProduct(Guid id, UpdateProductDto dto)
        {
			var product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id==id);
			if (product==null)
			{
				throw new NotFoundException(ExceptionMessages.ProductNotFound);
			}
			product.Name = dto.Name==null ? product.Name : dto.Name;
			product.Description = dto.Description==null ? product.Description : dto.Description;
			product.Price = dto.Price==null ? product.Price : dto.Price;
			product.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.ProductRepository.Update(product);
			await _unitOfWork.SaveAsync();
		}
    }
}
