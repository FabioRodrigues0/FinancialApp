using BuyRequest.Domain.Models.Validations;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Models;

namespace BuyRequest.Domain.Models;

public class BuyRequests : EntityBase<BuyRequests>
{
	public override async Task<bool> IsValid()
	{
		if (ValidationResult == null)
		{
			var validator = new BuyRequestValidations();
			ValidationResult = validator.Validate(this);
		}
		return ValidationResult?.IsValid != false;
	}

	public long Code { get; set; }
	public DateTimeOffset Date { get; set; }
	public DateTimeOffset DeliveryDate { get; set; }
	public Guid Client { get; set; }
	public string ClientDescription { get; set; }
	public string ClientEmail { get; set; }
	public string ClientPhone { get; set; }
	public Status Status { get; set; } = Status.Received;
	public string Street { get; set; }
	public string Number { get; set; }
	public string Sector { get; set; }
	public string Complement { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public decimal Discount { get; set; }
	public decimal ProductValor { get; set; }
	public decimal Cost { get; set; }
	public decimal TotalValor { get; set; }
	public List<BuyRequestProducts> Products { get; set; }
}
