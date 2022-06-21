using AutoMapper;
using Moq;
using Moq.AutoMock;
using Product.Api.Controllers;
using Product.Application.Application.Interface;
using Product.Application.DTO;
using Product.Application.Map;
using System.Threading.Tasks;
using Xunit;

namespace Product.UnitTest.ProductTest;

public class ProductsControllerTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public ProductsControllerTest()
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
	public async Task ProductsController_Post()
	{
		// Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var result = _mapper.Map<ProductsDto>(products);

		var application = _mocker.GetMock<IApplicationProductsService>();
		application.Setup(x => x.AddAsync(result));

		var controller = _mocker.CreateInstance<ProductsController>();

		// Act
		await controller.Post(result);

		// Assert
		application.Verify(x => x.AddAsync(It.IsAny<ProductsDto>()), Times.Once);
	}

	[Fact]
	public async Task ProductsController_Get()
	{
		// Arrange
		var application = _mocker.GetMock<IApplicationProductsService>();
		application.Setup(x => x.GetAllAsync(1));

		var controller = _mocker.CreateInstance<ProductsController>();

		// Act
		await controller.Get(1);

		// Assert
		application.Verify(x => x.GetAllAsync(1), Times.Once);
	}

	[Fact]
	public async Task ProductsController_Update()
	{
		// Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var result = _mapper.Map<ProductsWithIdDto>(products);

		var application = _mocker.GetMock<IApplicationProductsService>();
		application.Setup(x => x.UpdateAsync(result));

		var controller = _mocker.CreateInstance<ProductsController>();

		// Act
		await controller.Put(result);

		// Assert
		application.Verify(x => x.UpdateAsync(It.IsAny<ProductsWithIdDto>()), Times.Once);
	}

	[Fact]
	public async Task ProductsController_Remove()
	{
		// Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var result = _mapper.Map<ProductsWithIdDto>(products);

		var application = _mocker.GetMock<IApplicationProductsService>();
		application.Setup(x => x.RemoveAsync(result.Id));

		var controller = _mocker.CreateInstance<ProductsController>();

		// Act
		await controller.Delete(result.Id);

		// Assert
		application.Verify(x => x.RemoveAsync(result.Id), Times.Once);
	}

	[Fact]
	public async Task ProductsController_GetById()
	{
		// Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var result = _mapper.Map<ProductsWithIdDto>(products);

		var application = _mocker.GetMock<IApplicationProductsService>();
		application.Setup(x => x.GetByIdAsync(result.Id));

		var controller = _mocker.CreateInstance<ProductsController>();

		// Act
		await controller.Get(result.Id);

		// Assert
		application.Verify(x => x.GetByIdAsync(result.Id), Times.Once);
	}
}
