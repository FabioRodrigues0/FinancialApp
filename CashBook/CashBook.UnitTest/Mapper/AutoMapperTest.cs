using AutoMapper;
using CashBook.Application.DTO;
using CashBook.Application.Map;
using CashBook.UnitTest.CashBookTest;
using Moq.AutoMock;
using Shouldly;
using Xunit;

namespace CashBook.UnitTest.Mapper;

public class AutoMapperTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public AutoMapperTest()
	{
		_mocker = new AutoMocker();
		if (_mapper == null)
		{
			var mapConfig = new MapperConfiguration(x =>
			{
				x.AddProfile(new CashBookAutoMapper());
				x.AddProfile(new PagesCashBookMapper());
			});
			_mapper = mapConfig.CreateMapper();
		}
	}

	[Fact]
	public void AutoMapperCashBook()
	{
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookDto>(cashbook);

		result.ShouldNotBeNull();
		result.ShouldSatisfyAllConditions(
			() => result.Origin.ShouldBe(cashbook.Origin),
			() => result.OriginId.ShouldBe(cashbook.OriginId),
			() => result.Description.ShouldBe(cashbook.Description),
			() => result.Type.ShouldBe(cashbook.Type),
			() => result.Valor.ShouldBe(cashbook.Valor)
		);
	}

	[Fact]
	public void AutoMapperPageCashBook()
	{
		var cashBookFaker = new CashBookFaker();
		int totalPages = 1, page = 1;
		var cashbook = (cashBookFaker.listDto, totalPages, page);

		var result = _mapper.Map<PagesCashBookDto>(cashbook);

		result.ShouldNotBeNull();
	}
}
