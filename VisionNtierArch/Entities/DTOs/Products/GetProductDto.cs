namespace Entities.DTOs.Products
{
    public class GetProductDto:IDto
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
