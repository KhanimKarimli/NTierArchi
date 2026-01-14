
namespace Business.Validators.Products
{
    internal class UpdateProductDtoValidator : AbstractValidator<CreateProductDto>
	{
		public UpdateProductDtoValidator()
		{
			RuleFor(x => x.Name).NotEmpty()
				.MinimumLength(2).WithMessage("Product name must least 2 characters.");
		}
    }
}
