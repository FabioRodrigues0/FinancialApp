using FluentValidation;

namespace Document.Domain.Models.Validations;

public class DocumentValidations : AbstractValidator<Documents>
{
	public DocumentValidations()
	{
		RuleFor(x => x.Id).NotNull().WithMessage("Id can't be null");
		RuleFor(x => x.Number).NotNull().WithMessage("Number can't be null");
		RuleFor(x => x.Date).NotNull().WithMessage("Date can't be null");
		RuleFor(x => x.DocumentType)
			.NotNull().WithMessage("DocumentType can't be null")
			.IsInEnum();
		RuleFor(x => x.Operation)
			.NotNull().WithMessage("Operation can't be null")
			.IsInEnum();
		RuleFor(x => x.Paid).NotNull().WithMessage("Paid can't be null");
		RuleFor(x => x.PaymentDate).NotNull().WithMessage("PaymentDate can't be null");
		RuleFor(x => x.Description).NotNull().WithMessage("Description can't be null");
		RuleFor(x => x.Total).NotNull().WithMessage("Total can't be null");
	}
}