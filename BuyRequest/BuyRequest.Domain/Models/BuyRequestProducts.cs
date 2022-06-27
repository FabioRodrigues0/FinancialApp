using BuyRequest.Domain.Models.Validations;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Models;

namespace BuyRequest.Domain.Models;

public class BuyRequestProducts : EntityBase<BuyRequestProducts>
{
	public decimal Total { get; set; }

	public override async Task<bool> IsValid()
	{
		if (ValidationResult == null)
		{
			var validator = new BuyRequestProductsValidations();
			ValidationResult = validator.Validate(this);
		}

		return ValidationResult?.IsValid != false;
	}

	public virtual BuyRequests BuyRequest { get; set; }
	public Guid BuyRequestId { get; set; }
	public Guid ProductId { get; set; } = Guid.NewGuid();
	public string ProductDescription { get; set; }
	public ProductCategory ProductCategory { get; set; }
	public decimal Quantity { get; set; }
	public decimal Valor { get; set; }
}
