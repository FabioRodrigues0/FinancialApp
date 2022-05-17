using Infrastructure.Shared;
using Infrastructure.Shared.Enums;
using Product.Domain.Models.Validations;

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

	public override bool IsValid()
	{
		if (ValidationResult == null)
		{
			var validator = new ProductsValidations();
			ValidationResult = validator.Validate(this);
		}

		return ValidationResult?.IsValid != false;
	}
}
