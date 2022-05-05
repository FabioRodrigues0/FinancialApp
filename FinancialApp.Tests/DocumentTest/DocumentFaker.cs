﻿using System;
using Bogus;
using FinancialApp.Domain.Models;
using FinancialApp.Shared;
using FinancialApp.Shared.Enums;

namespace FinancialApp.Tests.DocumentTest;

public class DocumentFaker
{
	public Document document = new Faker<Document>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Number, x => x.Random.String(1, 256))
		.RuleFor(x => x.Date, x => DateTimeOffset.Now)
		.RuleFor(x => x.DocumentType, x => x.PickRandom<TypeDocument>())
		.RuleFor(x => x.Operation, x => x.PickRandom<Operation>())
		.RuleFor(x => x.Paid, x => x.Random.Bool())
		.RuleFor(x => x.PaymentDate, x => DateTimeOffset.Now.AddDays(2))
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.Observation, x => x.Random.String(1, 256))
		.RuleFor(x => x.Total, x => x.Random.Decimal(1, 30));
}