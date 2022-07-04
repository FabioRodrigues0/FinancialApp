using Infrastructure.Shared.Enums;

namespace BuyRequest.Application.Models;

public class BuyRequestUpdateModel
{
	public Guid Id { get; set; }
	public long Code { get; set; }
	public DateTimeOffset Date { get; set; }
	public DateTimeOffset DeliveryDate { get; set; }
	public Guid Client { get; set; }
	public string ClientDescription { get; set; }
	public string ClientEmail { get; set; }
	public string ClientPhone { get; set; }
	public Status Status { get; set; }
	public string Street { get; set; }
	public string Number { get; set; }
	public string Sector { get; set; }
	public string Complement { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public decimal Discount { get; set; }
	public decimal Cost { get; set; }
	public List<BuyRequestProductUpdateModel> Products { get; set; }
}
