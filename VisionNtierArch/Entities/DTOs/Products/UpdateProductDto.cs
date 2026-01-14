using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.Products
{
    public class UpdateProductDto:IDto
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
