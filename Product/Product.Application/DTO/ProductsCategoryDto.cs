using Infrastructure.Shared.Enums;

namespace Product.Application.DTO;

public class ProductsCategoryDto
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public ProductCategory Category { get; set; }
}