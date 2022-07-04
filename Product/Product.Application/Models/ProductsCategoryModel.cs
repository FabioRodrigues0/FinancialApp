using Infrastructure.Shared.Enums;

namespace Product.Application.Models;

public class ProductsCategoryModel
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public ProductCategory Category { get; set; }
}
