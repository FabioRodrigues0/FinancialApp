using Infrastructure.Shared.Enums;

namespace Product.Application.Models;

public class ProductsWithIdModel
{
	public Guid Id { get; set; }
	public string Code { get; set; }
	public string Description { get; set; }
	public ProductCategory Category { get; set; }
	public string NCM { get; set; }
	public string GTIN { get; set; }
	public string QRCode { get; set; }
}
