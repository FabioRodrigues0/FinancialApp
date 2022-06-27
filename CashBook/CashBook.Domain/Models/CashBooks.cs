using CashBook.Domain.Models.Validations;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Models;

namespace CashBook.Domain.Models;

public class CashBooks : EntityBase<CashBooks>
{
	public override async Task<bool> IsValid()
	{
		if (ValidationResult == null)
		{
			var validator = new CashBookValidations();
			ValidationResult = validator.Validate(this);
		}

		return ValidationResult?.IsValid != false;
	}

	public Origin Origin { get; set; }
	public Guid OriginId { get; set; }
	public string Description { get; set; }
	public StatusCashBook Type { get; set; }
	public decimal Valor { get; set; }
	public bool IsEdited { get; set; } = false;
}
