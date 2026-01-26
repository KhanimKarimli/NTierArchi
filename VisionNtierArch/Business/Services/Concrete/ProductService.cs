
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

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

        public async Task<IDataResult<List<GetAllProductsDto>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            if (products.Count==0)
            {
                return new ErrorDataResult<List<GetAllProductsDto>>(_mapper.Map<List<GetAllProductsDto>>(products), "Mehsul yoxdur");
            }
            return new SuccessDataResult<List<GetAllProductsDto>>(_mapper.Map<List<GetAllProductsDto>>(products), "Mehsullar siyahilandi");
		}

		public async Task<IDataResult<GetProductDto>> GetProductById(Guid id)
		{
			var product = await _unitOfWork.ProductRepository.GetAsync(p=>p.Id==id);
            if(product is null)
            {
                return new ErrorDataResult<GetProductDto>(_mapper.Map<GetProductDto>(product), "Mehsul tapilmadi");
            }
            return new SuccessDataResult<GetProductDto>(_mapper.Map<GetProductDto>(product), "Mehsul gonderildi");
		}
        
		public async Task<IResult> AddProduct(CreateProductDto dto)
        {
			var product =_mapper.Map<Product>(dto);
            product.CreatedAt = DateTime.UtcNow;
			await _unitOfWork.ProductRepository.AddAsync(product);
            var result =await _unitOfWork.SaveAsync();
            if(result==0)
            {
                return new ErrorResult("Mehsul elave edilmedi");
            }
            return new SuccessResult("Mehsul elave edildi");
        }

        public async Task<IResult> DeleteProductById(Guid id)
        {
			var product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id==id);
			if (product==null)
			{
				throw new NotFoundException(ExceptionMessages.ProductNotFound);
			}
			_unitOfWork.ProductRepository.Delete(product);
			var result =await _unitOfWork.SaveAsync();
			if (result==0)
			{
				return new ErrorResult("Mehsul siline bilmedi");
			}
			return new SuccessResult("Mehsul silindi");
		}


        public async Task<IResult> UpdateProduct(Guid id, UpdateProductDto dto)
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
			var result =await _unitOfWork.SaveAsync();
			if (result==0)
			{
				return new ErrorResult("Mehsul siline bilmedi");
			}
			return new SuccessResult("Mehsul silindi");
		}
    }
}
