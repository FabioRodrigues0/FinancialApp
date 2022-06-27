using FluentValidation;
using Product.Domain.Models;

namespace Product.Domain.Validations;

public class ProductsValidations : AbstractValidator<Products>
{
	public ProductsValidations()
	{
		RuleFor(x => x.Id).NotNull().WithMessage("Id can't be null");
		RuleFor(x => x.Code);
		RuleFor(x => x.Description)
			.NotNull().WithMessage("Description can't be null");
		RuleFor(x => x.Category)
			.NotNull().WithMessage("Category can't be null")
			.IsInEnum();
		RuleFor(x => x.NCM);
		RuleFor(x => x.GTIN)
			.NotNull().WithMessage("GTIN can't be null");
		RuleFor(x => x.QRCode);
	}
}
