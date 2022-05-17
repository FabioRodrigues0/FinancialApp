using AutoMapper;
using CashBook.Api.Controllers;
using CashBook.Application.Application.Interface;
using CashBook.Application.DTO;
using CashBook.Application.Map;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace CashBook.UnitTest.CashBookTest;

public class CashBookControllerTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public CashBookControllerTest()
	{
		_mocker = new AutoMocker();
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
	public async Task CashBookController_Post()
	{
		// Arrange
		var cashBookFaker = new CashBookFaker();
		var cashBook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookDto>(cashBook);

		var application = _mocker.GetMock<IApplicationCashBookService>();
		application.Setup(x => x.AddAsync(result));

		var controller = _mocker.CreateInstance<CashBookController>();

		// Act
		await controller.Post(result);

		// Assert
		application.Verify(x => x.AddAsync(It.IsAny<CashBookDto>()), Times.Once);
	}

	[Fact]
	public async Task CashBookController_Get()
	{
		// Arrange
		var application = _mocker.GetMock<IApplicationCashBookService>();
		application.Setup(x => x.GetAllAsync(1));

		var controller = _mocker.CreateInstance<CashBookController>();

		// Act
		await controller.Get(1);

		// Assert
		application.Verify(x => x.GetAllAsync(1), Times.Once);
	}

	[Fact]
	public async Task CashBookController_Update()
	{
		// Arrange
		var cashBookFaker = new CashBookFaker();
		var cashBook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookUpdateDto>(cashBook);

		var application = _mocker.GetMock<IApplicationCashBookService>();
		application.Setup(x => x.UpdateAsync(result));

		var controller = _mocker.CreateInstance<CashBookController>();

		// Act
		await controller.Put(result);

		// Assert
		application.Verify(x => x.UpdateAsync(It.IsAny<CashBookUpdateDto>()), Times.Once);
	}

	[Fact]
	public async Task CashBookController_GetById()
	{
		// Arrange
		var cashBookFaker = new CashBookFaker();
		var cashBook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookUpdateDto>(cashBook);

		var application = _mocker.GetMock<IApplicationCashBookService>();
		application.Setup(x => x.GetByIdAsync(result.Id));

		var controller = _mocker.CreateInstance<CashBookController>();

		// Act
		await controller.Get(result.Id);

		// Assert
		application.Verify(x => x.GetByIdAsync(result.Id), Times.Once);
	}

	[Fact]
	public async Task CashBookController_GetByOriginId()
	{
		// Arrange
		var cashBookFaker = new CashBookFaker();
		var cashBook = cashBookFaker.cashbook;

		var result = _mapper.Map<CashBookUpdateDto>(cashBook);

		var application = _mocker.GetMock<IApplicationCashBookService>();
		application.Setup(x => x.GetByOriginIdAsync(result.OriginId));

		var controller = _mocker.CreateInstance<CashBookController>();

		// Act
		await controller.GetOriginId(result.OriginId);

		// Assert
		application.Verify(x => x.GetByOriginIdAsync(result.OriginId), Times.Once);
	}
}