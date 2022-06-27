using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Models;
using Product.Domain.Validations;

namespace Product.Domain.Models;

public class Products : EntityBase<Products>
{
	public Guid Id { get; set; }
	public string Code { get; set; }
	public string Description { get; set; }
	public ProductCategory Category { get; set; }
	public string NCM { get; set; }
	public string GTIN { get; set; }
	public string QRCode { get; set; }

	public override async Task<bool> IsValid()
	{
		if (ValidationResult == null)
		{
			var validator = new ProductsValidations();
			ValidationResult = await validator.ValidateAsync(this);
		}

		return ValidationResult?.IsValid != false;
	}
}
