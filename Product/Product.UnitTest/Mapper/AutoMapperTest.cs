using AutoMapper;
using Moq.AutoMock;
using Product.Application.DTO;
using Product.Application.Map;
using Product.UnitTest.ProductTest;
using Shouldly;
using Xunit;

namespace Product.UnitTest.Mapper;

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
				x.AddProfile(new ProductsAutoMapper());
			});
			_mapper = mapConfig.CreateMapper();
		}
	}

	[Fact]
	public void AutoMapperDocument()
	{
		var documentFaker = new ProductsFaker();
		var products = documentFaker.products;

		var result = _mapper.Map<ProductsDto>(products);

		result.ShouldNotBeNull();
		result.ShouldSatisfyAllConditions(
			() => result.Code.ShouldBe(products.Code),
			() => result.Description.ShouldBe(products.Description),
			() => result.Category.ShouldBe(products.Category),
			() => result.GTIN.ShouldBe(products.GTIN),
			() => result.NCM.ShouldBe(products.NCM),
			() => result.QRCode.ShouldBe(products.QRCode));
	}

	[Fact]
	public void AutoMapperPageProducts()
	{
		var config = new MapperConfiguration(cfg => cfg.AddProfile<PagesProductsMapper>());
		config.AssertConfigurationIsValid();
	}
}