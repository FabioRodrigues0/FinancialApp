using FluentValidation;

namespace BuyRequest.Domain.Entities.Validations;

public class BuyRequestProductsValidations : AbstractValidator<BuyRequestProducts>
{
	public BuyRequestProductsValidations()
	{
		RuleFor(x => x.Id).NotNull();
		RuleFor(x => x.BuyRequestId).NotNull();
		RuleFor(x => x.ProductId).NotNull();
		RuleFor(x => x.ProductDescription).NotNull();
		RuleFor(x => x.ProductCategory)
			.NotNull()
			.IsInEnum();
		RuleFor(x => x.Quantity).NotNull();
		RuleFor(x => x.Valor).NotNull();
	}
}