using AutoMapper;
using BuyRequest.Application.Application;
using BuyRequest.Application.Map;
using BuyRequest.Application.Models;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Models;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BuyRequest.UnitTest.BuyRequestTest;

public class BuyRequestApplicationServiceTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public BuyRequestApplicationServiceTest()
	{
		_mocker = new AutoMocker();
		if (_mapper == null)
		{
			var mapConfig = new MapperConfiguration(x =>
			{
				x.AddProfile(new BuyRequestAutoMapper());
				x.AddProfile(new BuyRequestProductAutoMapper());
			});
			_mapper = mapConfig.CreateMapper();
		}
	}

	[Fact]
	public async Task BuyRequestApplicationService_GetAll()
	{
		//Arrange
		var buyRequestFaker = new BuyRequestFaker();
		var pageBuyRequest = buyRequestFaker.pageBuyRequest;
		int totalPages = 1, page = 1;
		var list = new PagesBase<BuyRequests> { };

		var repository = _mocker.GetMock<IBuyRequestService>();
		repository.Setup(x => x.GetAllAsync(page, 10)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<ApplicationBuyRequestService>();

		//Act
		await service.GetAllAsync(page, 10);

		//Assert
		repository.Verify(x => x.GetAllAsync(page, 10), Times.Once);
	}

	[Fact]
	public async Task BuyRequestApplicationService_GetById()
	{
		//Arrange
		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestService>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Id));

		var service = _mocker.CreateInstance<ApplicationBuyRequestService>();

		//Act
		await service.GetByIdAsync(buyRequest.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(buyRequest.Id), Times.Once);
	}

	[Fact]
	public async Task BuyRequestApplicationService_GetByIdWithClient()
	{
		//Arrange
		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestService>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Client));

		var service = _mocker.CreateInstance<ApplicationBuyRequestService>();

		//Act
		await service.GetByIdAsync(buyRequest.Client);

		//Assert
		repository.Verify(x => x.GetByIdAsync(buyRequest.Client), Times.Once);
	}

	[Fact]
	public async Task BuyRequestApplicationService_Add()
	{
		//Arrange

		#region Vars

		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var result = _mapper.Map<BuyRequestModel>(buyRequest);

		var repository = _mocker.GetMock<IBuyRequestService>();
		repository.Setup(x => x.AddAsync(buyRequest));

		var service = _mocker.CreateInstance<ApplicationBuyRequestService>();

		#endregion Vars

		//Act
		await service.AddAsync(result);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<BuyRequests>()), Times.Once);
	}

	[Fact]
	public async Task BuyRequestApplicationService_Update()
	{
		//Arrange

		#region Vars

		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var result = _mapper.Map<BuyRequestUpdateModel>(buyRequest);

		var repository = _mocker.GetMock<IBuyRequestService>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Id)).ReturnsAsync(buyRequest);
		repository.Setup(x => x.UpdateAsync(buyRequest));

		var service = _mocker.CreateInstance<ApplicationBuyRequestService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(result);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<BuyRequests>()), Times.Once);
	}

	[Fact]
	public async Task BuyRequestApplicationService_Patch()
	{
		//Arrange

		#region Vars

		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var result = _mapper.Map<BuyRequestPatchModel>(buyRequest);

		var repository = _mocker.GetMock<IBuyRequestService>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Id)).ReturnsAsync(buyRequest);
		repository.Setup(x => x.PatchAsync(buyRequest));

		var service = _mocker.CreateInstance<ApplicationBuyRequestService>();

		#endregion Vars

		//Act
		await service.PatchAsync(result);

		//Assert
		repository.Verify(x => x.PatchAsync(It.IsAny<BuyRequests>()), Times.Once);
	}
}
