

namespace Business.Utilities.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, GetAllProductsDto>();
			CreateMap<Product, GetProductDto>();
			CreateMap<CreateProductDto, Product>(); 
		}
    }
}
