using CashBook.Application.Services;
using CashBook.Data.Repositories.Interfaces;
using CashBook.Domain.Entities;
using Infrastructure.Shared.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace CashBook.UnitTest.CashBookTest;

public class CashBookServiceTest
{
	public readonly AutoMocker Mocker;

	public CashBookServiceTest()
	{
		Mocker = new AutoMocker();
	}

	[Fact]
	public async Task CashBookService_GetAll()
	{
		//Arrange
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookRepository>();
		int totalPages = 1, page = 1;
		var list = new PagesBase<CashBooks> { };
		repository.Setup(x => x.GetAllAsync(page, 10)).ReturnsAsync(list);

		var service = Mocker.CreateInstance<CashBookService>();

		//Act
		await service.GetAllAsync(1, 10);

		//Assert
		repository.Verify(x => x.GetAllAsync(1, 10), Times.Once);
	}

	[Fact]
	public async Task CashBookService_GetById()
	{
		//Arrange
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookRepository>();
		repository.Setup(x => x.GetByIdAsync(cashbook.Id));

		var service = Mocker.CreateInstance<CashBookService>();

		//Act
		await service.GetByIdAsync(cashbook.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(cashbook.Id), Times.Once);
	}

	[Fact]
	public async Task CashBookService_GetByOriginId()
	{
		//Arrange
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookRepository>();
		repository.Setup(x => x.GetByOriginId(cashbook.OriginId));

		var service = Mocker.CreateInstance<CashBookService>();

		//Act
		await service.GetByOriginIdAsync(cashbook.OriginId);

		//Assert
		repository.Verify(x => x.GetByOriginId(cashbook.OriginId), Times.Once);
	}

	[Fact]
	public async Task CashBookService_Add()
	{
		//Arrange

		#region Vars

		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookRepository>();
		repository.Setup(x => x.AddAsync(cashbook));

		var service = Mocker.CreateInstance<CashBookService>();

		#endregion Vars

		//Act
		await service.AddAsync(cashbook);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<CashBooks>()), Times.Once);
	}

	[Fact]
	public async Task CashBookService_Update()
	{
		//Arrange

		#region Vars

		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookRepository>();
		repository.Setup(x => x.GetByIdAsync(cashbook.Id)).ReturnsAsync(cashbook);
		repository.Setup(x => x.UpdateAsync(cashbook));

		var test = repository.Setups;

		var service = Mocker.CreateInstance<CashBookService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(cashbook);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<CashBooks>()), Times.Once);
	}
}
