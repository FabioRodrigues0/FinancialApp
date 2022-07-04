using Bogus;
using CashBook.Application.Models;
using CashBook.Domain.Entities;
using Infrastructure.Shared.Enums;
using System;
using System.Collections.Generic;

namespace CashBook.UnitTest.CashBookTest;

public class CashBookFaker
{
	public CashBooks cashbook = new Faker<CashBooks>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Origin, x => x.PickRandom<Origin>())
		.RuleFor(x => x.OriginId, Guid.NewGuid)
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.Type, x => x.PickRandom<StatusCashBook>())
		.RuleFor(x => x.Valor, x => x.Random.Decimal(1, 30))
		.RuleFor(x => x.IsEdited, true);

	public List<CashBookModel> listDto = new Faker<CashBookModel>()
			.RuleFor(x => x.Origin, x => x.PickRandom<Origin>())
			.RuleFor(x => x.OriginId, Guid.NewGuid)
			.RuleFor(x => x.Description, x => x.Random.String(1, 256))
			.RuleFor(x => x.Type, x => x.PickRandom<StatusCashBook>())
			.RuleFor(x => x.Valor, x => x.Random.Decimal(99E-2m, 3E1m))
			.GenerateBetween(1, 3);

	public List<CashBooks> listModel = new Faker<CashBooks>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Origin, x => x.PickRandom<Origin>())
		.RuleFor(x => x.OriginId, Guid.NewGuid)
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.Type, x => x.PickRandom<StatusCashBook>())
		.RuleFor(x => x.Valor, x => x.Random.Decimal(99E-2m, 3E1m))
		.RuleFor(x => x.IsEdited, true)
		.GenerateBetween(1, 3);
}
