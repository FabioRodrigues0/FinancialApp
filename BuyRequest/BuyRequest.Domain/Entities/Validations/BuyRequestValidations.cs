using FluentValidation;
using Infrastructure.Shared.Enums;

namespace BuyRequest.Domain.Entities.Validations;

public class BuyRequestValidations : AbstractValidator<BuyRequests>
{
	public BuyRequestValidations()
	{
		RuleFor(x => x.Id).NotNull().WithMessage("Id can't be null");
		RuleFor(x => x.Code).NotNull().WithMessage("Code can't be null");
		RuleFor(x => x.Date).NotNull().WithMessage("Date can't be null");
		RuleFor(x => x.DeliveryDate).NotNull().When(x => x.Status == Status.Finished).WithMessage("Delivery Date can't be null");
		RuleForEach(x => x.Products)
			.SetValidator(new BuyRequestProductsValidations());
		RuleFor(x => x.Products).Must(x => x.Any()).WithMessage("Products can't be null");
		RuleForEach(x => x.Products).Must(IsValidCategory).WithMessage("Products can't have ProductsCategory of type Digital(1) and Physical(2)");
		RuleFor(x => x.Client).NotNull().WithMessage("Client can't be null");
		RuleFor(x => x.ClientDescription).NotNull().WithMessage("Client Description can't be null");
		RuleFor(x => x.ClientEmail)
			.NotNull().WithMessage("Client Email can't be null")
			.EmailAddress();
		RuleFor(x => x.ClientPhone)
			.NotNull().WithMessage("Client Phone can't be null")
			.MinimumLength(9).WithMessage("Client Phone has to be a phone number");
		RuleFor(x => x.Status)
			.NotNull().WithMessage("Status can't be null")
			.Must(IsValidStatus).WithMessage("If is a Physical Product must select AwaitingDelivery(2) or if is a Digital Product must select AwaitingDownload(3)")
			.IsInEnum();
		RuleFor(x => x.ProductValor).NotNull().WithMessage("Product Valor can't be null");
		RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount can't be negative");
		RuleFor(x => x.Cost).NotNull().WithMessage("Cost can't be null");
		RuleFor(x => x.TotalValor)
			.NotNull().WithMessage("Total Valor can't be null");
	}

	private bool IsValidCategory(BuyRequests p, BuyRequestProducts pq)
	{
		var productD = p.Products.Count(y => y.ProductCategory == ProductCategory.Digital);
		var productP = p.Products.Count(y => y.ProductCategory == ProductCategory.Physical);
		var products = p.Products.Count();
		if (productD == products && productP == 0 || productP == products && productD == 0)
			return true;
		return false;
	}

	private bool IsValidStatus(BuyRequests p, Status status)
	{
		var status1 = status == Status.Received;
		var status2 = status == Status.AwaitingDelivery;
		var status3 = status == Status.AwaitingDownload;
		var status4 = status == Status.Finished;
		var productD = p.Products.Count(y => y.ProductCategory == ProductCategory.Digital);
		var productP = p.Products.Count(y => y.ProductCategory == ProductCategory.Physical);
		var products = p.Products.Count();
		if (productD == products && productP == 0 && status3 ||
		   productP == products && productD == 0 && status2 ||
		   productP == products && productD == 0 && status4 ||
		   productD == products && productP == 0 && status4 ||
		   productP == products && productD == 0 && status1 ||
		   productD == products && productP == 0 && status1)
			return true;
		return false;
	}
}