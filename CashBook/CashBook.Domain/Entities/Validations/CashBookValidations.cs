using FluentValidation;
using Infrastructure.Shared.Enums;

namespace CashBook.Domain.Entities.Validations;

public class CashBookValidations : AbstractValidator<CashBooks>
{
	public CashBookValidations()
	{
		RuleFor(x => x.Origin).NotNull().When(x => x.IsEdited = false).WithMessage("Origin can't be null");
		RuleFor(x => x.OriginId).NotNull().When(x => x.IsEdited = false).WithMessage("OriginId can't be null");
		RuleFor(x => x.Description).NotNull().WithMessage("Description can't be null");
		RuleFor(x => x.Type).NotNull().WithMessage("Type can't be null");
		RuleFor(x => x.Valor)
			.NotNull().WithMessage("Valor can't be null")
			.Must(IsValidValor).WithMessage("Valor can't be a positive number if Type is 1 or can't be negative if is 2");
	}

	private bool IsValidValor(CashBooks p, decimal valor)
	{
		var typeP = StatusCashBook.Payment;
		var typeR = StatusCashBook.Receivement;
		if (p.Type == typeP && valor < 0 || typeR == p.Type && valor > 0)
			return true;
		return false;
	}
}