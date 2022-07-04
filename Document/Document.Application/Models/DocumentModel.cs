using Infrastructure.Shared.Enums;

namespace Document.Application.Models;

public class DocumentModel
{
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
