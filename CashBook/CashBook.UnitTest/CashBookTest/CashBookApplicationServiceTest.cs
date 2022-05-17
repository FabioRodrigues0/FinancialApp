using System.Collections.Generic;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;
using CashBook.Application.Map;
using CashBook.Application.Services.Interface;
using CashBook.Domain.Models;
using CashBook.Application.Application;
using CashBook.Application.DTO;

namespace CashBook.UnitTest.CashBookTest;

public class CashBookApplicationServiceTest
{
	public readonly AutoMocker Mocker;
	private static IMapper _mapper;

	public CashBookApplicationServiceTest()
	{
		Mocker = new AutoMocker();
		if (_mapper == null)
		{
			var mapConfig = new MapperConfiguration(x =>
			{
				x.AddProfile(new CashBookAutoMapper());
			});
			_mapper = mapConfig.CreateMapper();
		}
	}

	[Fact]
	public async Task CashBookApplicationService_GetAll()
	{
		//Arrange
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;
		int totalPages = 1, page = 1;
		var list = (new List<CashBooks>(), totalPages, page);

		var repository = Mocker.GetMock<ICashBookService>();
		repository.Setup(x => x.GetAllAsync(page)).ReturnsAsync(list);

		var service = Mocker.CreateInstance<ApplicationCashBookService>();

		//Act
		await service.GetAllAsync(page);

		//Assert
		repository.Verify(x => x.GetAllAsync(page), Times.Once);
	}

	[Fact]
	public async Task CashBookApplicationService_GetById()
	{
		//Arrange
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookService>();
		repository.Setup(x => x.GetByIdAsync(cashbook.Id));

		var service = Mocker.CreateInstance<ApplicationCashBookService>();

		//Act
		await service.GetByIdAsync(cashbook.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(cashbook.Id), Times.Once);
	}

	[Fact]
	public async Task CashBookApplicationService_GetByOriginId()
	{
		//Arrange
		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var repository = Mocker.GetMock<ICashBookService>();
		repository.Setup(x => x.GetByOriginIdAsync(cashbook.OriginId));

		var service = Mocker.CreateInstance<ApplicationCashBookService>();

		//Act
		await service.GetByOriginIdAsync(cashbook.OriginId);

		//Assert
		repository.Verify(x => x.GetByOriginIdAsync(cashbook.OriginId), Times.Once);
	}

	[Fact]
	public async Task CashBookApplicationService_Add()
	{
		//Arrange

		#region Vars

		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookDto>(cashbook);

		var repository = Mocker.GetMock<ICashBookService>();
		repository.Setup(x => x.AddAsync(cashbook));

		var service = Mocker.CreateInstance<ApplicationCashBookService>();

		#endregion Vars

		//Act
		await service.AddAsync(result);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<CashBooks>()), Times.Once);
	}

	[Fact]
	public async Task CashBookApplicationService_Update()
	{
		//Arrange

		#region Vars

		var cashBookFaker = new CashBookFaker();
		var cashbook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookUpdateDto>(cashbook);

		var repository = Mocker.GetMock<ICashBookService>();
		repository.Setup(x => x.GetByIdAsync(cashbook.Id)).ReturnsAsync(cashbook);
		repository.Setup(x => x.UpdateAsync(cashbook));

		var service = Mocker.CreateInstance<ApplicationCashBookService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(result);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<CashBooks>()), Times.Once);
	}
}