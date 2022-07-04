using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Enums;
using Moq;
using Moq.AutoMock;
using Product.Application.Services;
using Product.Data.Repositories.Interface;
using Product.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace Product.UnitTest.ProductTest;

public class ProductsServiceTest
{
	public readonly AutoMocker _mocker;

	public ProductsServiceTest()
	{
		_mocker = new AutoMocker();
	}

	[Fact]
	public async Task ProductsService_GetAll()
	{
		//Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;
		int totalPages = 1, page = 1;
		var list = new PagesBase<Products> { };

		var repository = _mocker.GetMock<IProductsRepository>();
		repository.Setup(x => x.GetAllAsync(page, 10)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<ProductsService>();

		//Act
		await service.GetAllAsync(1, 10);

		//Assert
		repository.Verify(x => x.GetAllAsync(1, 10), Times.Once);
	}

	[Fact]
	public async Task ProductsService_GetByCategory()
	{
		//Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;
		int totalPages = 1, page = 1;
		var list = new PagesBase<Products> { };
		var category = ProductCategory.Physical;

		var repository = _mocker.GetMock<IProductsRepository>();
		repository.Setup(x => x.GetAllAsync(x => x.Category == category, page, 10)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<ProductsService>();

		//Act
		await service.GetByCategoryAsync(category, 1, 10);

		//Assert
		repository.Verify(x => x.GetAllAsync(x => x.Category == category, page, 10), Times.Once);
	}

	[Fact]
	public async Task ProductsService_GetById()
	{
		//Arrange
		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var repository = _mocker.GetMock<IProductsRepository>();
		repository.Setup(x => x.GetByIdAsync(products.Id));

		var service = _mocker.CreateInstance<ProductsService>();

		//Act
		await service.GetByIdAsync(products.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(products.Id), Times.Once);
	}

	[Fact]
	public async Task ProductsService_Add()
	{
		//Arrange

		#region Vars

		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var repository = _mocker.GetMock<IProductsRepository>();
		repository.Setup(x => x.AddAsync(products));

		var service = _mocker.CreateInstance<ProductsService>();

		#endregion Vars

		//Act
		await service.AddAsync(products);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<Products>()), Times.Once);
	}

	[Fact]
	public async Task ProductsService_Update()
	{
		//Arrange

		#region Vars

		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var repository = _mocker.GetMock<IProductsRepository>();
		repository.Setup(x => x.UpdateAsync(products));

		var service = _mocker.CreateInstance<ProductsService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(products);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<Products>()), Times.Once);
	}

	[Fact]
	public async Task ProductsService_Patch()
	{
		//Arrange

		#region Vars

		var productsFaker = new ProductsFaker();
		var products = productsFaker.products;

		var repository = _mocker.GetMock<IProductsRepository>();
		var repositoryId = repository.Setup(x => x.GetByIdAsync(products.Id)).ReturnsAsync(products);
		repository.Setup(x => x.PatchAsync(products)).ReturnsAsync(products);

		var service = _mocker.CreateInstance<ProductsService>();

		#endregion Vars

		//Act
		await service.PatchAsync(products);

		//Assert
		repository.Verify(x => x.PatchAsync(It.IsAny<Products>()), Times.Once);
	}
}
