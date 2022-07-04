using Document.Domain.Models.Validations;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Enums;

namespace Document.Domain.Models;

public class Documents : EntityBase<Documents>
{
	public override async Task<bool> IsValid()
	{
		if (ValidationResult == null)
		{
			var validator = new DocumentValidations();
			ValidationResult = validator.Validate(this);
		}

		return ValidationResult?.IsValid != false;
	}

	public string Number { get; set; }
	public DateTimeOffset Date { get; set; }
	public TypeDocument DocumentType { get; set; }
	public Operation Operation { get; set; }
	public bool Paid { get; set; }
	public DateTimeOffset PaymentDate { get; set; }
	public string Description { get; set; }
	public decimal Total { get; set; }
	public string Observation { get; set; }
}
