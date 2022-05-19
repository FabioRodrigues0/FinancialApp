using Infrastructure.Shared.Enums;

namespace Product.Application.DTO;

public class ProductsWithIdDto
{
	public Guid Id { get; set; }
	public string Code { get; set; }
	public string Description { get; set; }
	public ProductCategory Category { get; set; }
	public string NCM { get; set; }
	public string GTIN { get; set; }
	public string QRCode { get; set; }
}