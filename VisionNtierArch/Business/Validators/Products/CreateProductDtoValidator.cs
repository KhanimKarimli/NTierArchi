using Entities.DTOs.Products;
using FluentValidation;


namespace Business.Validators.Products
{
    public class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
	{
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(2).WithMessage("Product name must least 2 characters.");
		}
    }
}
