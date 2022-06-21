using Bogus;
using CashBook.Application.DTO;
using CashBook.Domain.Models;
using Infrastructure.Shared.Enums;
using System;

namespace MessageBroker.UnitTest;

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

	public CashBookDto cashbookDto = new Faker<CashBookDto>()
		.RuleFor(x => x.Origin, x => x.PickRandom<Origin>())
		.RuleFor(x => x.OriginId, Guid.NewGuid)
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.Type, x => x.PickRandom<StatusCashBook>())
		.RuleFor(x => x.Valor, x => x.Random.Decimal(1, 30))
		.RuleFor(x => x.IsEdited, true);
}
