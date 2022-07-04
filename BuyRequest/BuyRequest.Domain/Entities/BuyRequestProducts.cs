using BuyRequest.Domain.Entities.Validations;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Enums;

namespace BuyRequest.Domain.Entities;

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
