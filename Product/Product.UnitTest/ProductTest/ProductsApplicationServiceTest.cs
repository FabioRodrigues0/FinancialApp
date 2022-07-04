using AutoMapper;
using Infrastructure.Shared.Entities;
using Moq;
using Moq.AutoMock;
using Product.Application.Application;
using Product.Application.Map;
using Product.Application.Models;
using Product.Application.Services.Interface;
using Product.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace Product.UnitTest.ProductTest;

public class ProductsApplicationServiceTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public ProductsApplicationServiceTest()
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
	public async Task ProductsApplicationService_GetAll()
	{
		//Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;
		int totalPages = 1, page = 1;
		var list = new PagesBase<Products> { };

		var repository = _mocker.GetMock<IProductsService>();
		repository.Setup(x => x.GetAllAsync(page, 10)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<ApplicationProductsService>();

		//Act
		await service.GetAllAsync(page, 10);

		//Assert
		repository.Verify(x => x.GetAllAsync(page, 10), Times.Once);
	}

	[Fact]
	public async Task ProductsApplicationService_GetById()
	{
		//Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var repository = _mocker.GetMock<IProductsService>();
		repository.Setup(x => x.GetByIdAsync(products.Id));

		var service = _mocker.CreateInstance<ApplicationProductsService>();

		//Act
		await service.GetByIdAsync(products.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(products.Id), Times.Once);
	}

	[Fact]
	public async Task ProductsApplicationService_Add()
	{
		//Arrange

		#region Vars

		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var result = _mapper.Map<ProductsModel>(products);

		var repository = _mocker.GetMock<IProductsService>();
		repository.Setup(x => x.AddAsync(products));

		var service = _mocker.CreateInstance<ApplicationProductsService>();

		#endregion Vars

		//Act
		await service.AddAsync(result);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<Products>()), Times.Once);
	}

	[Fact]
	public async Task ProductsApplicationService_Update()
	{
		//Arrange

		#region Vars

		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var result = _mapper.Map<ProductsWithIdModel>(products);

		var repository = _mocker.GetMock<IProductsService>();
		repository.Setup(x => x.UpdateAsync(products));

		var service = _mocker.CreateInstance<ApplicationProductsService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(result);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<Products>()), Times.Once);
	}
}
