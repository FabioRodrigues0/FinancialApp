using AutoMapper;
using BuyRequest.Application.DTO;
using BuyRequest.Application.Map;
using BuyRequest.UnitTest.BuyRequestTest;
using Moq.AutoMock;
using Shouldly;
using Xunit;

namespace BuyRequest.UnitTest.Mapper;

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
				x.AddProfile(new BuyRequestAutoMapper());
				x.AddProfile(new BuyRequestProductAutoMapper());
				x.AddProfile(new PagesBuyRequestMapper());
			});
			_mapper = mapConfig.CreateMapper();
		}
	}

	[Fact]
	public void AutoMapperBuyRequestProduct()
	{
		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = BuyRequestFaker.buyRequestProducts.Generate();

		var result = _mapper.Map<BuyRequestProductDto>(buyRequest);

		result.ShouldNotBeNull();
		result.ShouldSatisfyAllConditions(
			() => result.ProductDescription.ShouldBe(buyRequest.ProductDescription),
			() => result.ProductCategory.ShouldBe(buyRequest.ProductCategory),
			() => result.Quantity.ShouldBe(buyRequest.Quantity),
			() => result.Valor.ShouldBe(buyRequest.Valor)
			);
	}

	[Fact]
	public void AutoMapperBuyRequest()
	{
		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var result = _mapper.Map<BuyRequestDto>(buyRequest);

		result.ShouldNotBeNull();
		result.ShouldSatisfyAllConditions(
			() => result.Code.ShouldBe(buyRequest.Code),
			() => result.Date.ShouldBe(buyRequest.Date),
			() => result.DeliveryDate.ShouldBe(buyRequest.DeliveryDate),
			() => result.Client.ShouldBe(buyRequest.Client),
			() => result.ClientDescription.ShouldBe(buyRequest.ClientDescription),
			() => result.ClientEmail.ShouldBe(buyRequest.ClientEmail),
			() => result.ClientPhone.ShouldBe(buyRequest.ClientPhone),
			() => result.Status.ShouldBe(buyRequest.Status),
			() => result.Discount.ShouldBe(buyRequest.Discount),
			() => result.Cost.ShouldBe(buyRequest.Cost)
		//() => result.Products.ShouldBeEquivalentTo(buyRequest.Products)
		);
	}

	[Fact]
	public void AutoMapperPageBuyRequest()
	{
		var config = new MapperConfiguration(cfg => cfg.AddProfile<PagesBuyRequestMapper>());
		config.AssertConfigurationIsValid();
	}
}
