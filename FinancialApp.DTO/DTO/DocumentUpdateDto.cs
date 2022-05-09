﻿using FinancialApp.Shared.Enums;

namespace FinancialApp.DTO.DTO;

public class DocumentUpdateDto
{
	public Guid Id { get; set; }
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