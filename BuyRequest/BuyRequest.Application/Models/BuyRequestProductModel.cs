using Infrastructure.Shared.Enums;

namespace BuyRequest.Application.Models;

public class BuyRequestProductModel
{
	public string ProductDescription { get; set; }
	public ProductCategory ProductCategory { get; set; }
	public decimal Quantity { get; set; }
	public decimal Valor { get; set; }
}
