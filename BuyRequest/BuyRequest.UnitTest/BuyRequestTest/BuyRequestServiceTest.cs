using BuyRequest.Application.Services;
using BuyRequest.Data.Repositories.Interfaces;
using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Messaging.Settings;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace BuyRequest.UnitTest.BuyRequestTest;

public class BuyRequestServiceTest
{
	public readonly AutoMocker _mocker;
	public readonly string queueName;
	public readonly string queueNameMP;
	private readonly string connectionString;
	private readonly string topic;
	private readonly string routingKey;
	private readonly RabbitMqOptions _appSettings;

	public BuyRequestServiceTest()
	{
		_mocker = new AutoMocker();
		queueName = "queueTest";
		queueNameMP = "queueMPTest";
		connectionString = "localhost";
		topic = "topicTest";
		routingKey = "routingKeyTest";
		_appSettings = new RabbitMqOptions() {
			QueueName = queueName,
			QueueNameMP = queueNameMP,
			StringConnection = connectionString,
			Topic = topic,
			RoutingKey = routingKey,
		};
	}

	[Fact]
	public async Task BuyRequestService_GetAll()
	{
		//Arrange
		var buyRequestFaker = new BuyRequestFaker();
		int totalPages = 1, page = 1;
		var list = (buyRequestFaker.listModel, totalPages, page);

		var repository = _mocker.GetMock<IBuyRequestRepository>();
		repository.Setup(x => x.GetAllAsync(page)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<BuyRequestService>();

		//Act
		await service.GetAllAsync(page);

		//Assert
		repository.Verify(x => x.GetAllAsync(page), Times.Once);
	}

	[Fact]
	public async Task BuyRequestService_GetById()
	{
		//Arrange
		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestRepository>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Id));

		var service = _mocker.CreateInstance<BuyRequestService>();

		//Act
		await service.GetByIdAsync(buyRequest.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(buyRequest.Id), Times.Once);
	}

	[Fact]
	public async Task BuyRequestService_GetByIdWithClient()
	{
		//Arrange
		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestRepository>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Client));

		var service = _mocker.CreateInstance<BuyRequestService>();

		//Act
		await service.GetByIdAsync(buyRequest.Client);

		//Assert
		repository.Verify(x => x.GetByIdAsync(buyRequest.Client), Times.Once);
	}

	[Fact]
	public async Task BuyRequestService_Add()
	{
		//Arrange

		#region Vars

		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestRepository>();
		var optionsV = _mocker.GetMock<IOptions<RabbitMqOptions>>();
		var options = _mocker.GetMock<RabbitMqOptions>();
		
		repository.Setup(x => x.AddAsync(buyRequest));

		var mockoptions = Options();
		mockoptions.Setup(ap => ap.Value).Returns(_appSettings);


		var service = _mocker.CreateInstance<BuyRequestService>();

		#endregion Vars

		//Act
		await service.AddAsync(buyRequest);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<BuyRequests>()), Times.Once);
	}

	[Fact]
	public async Task BuyRequestService_Update()
	{
		//Arrange

		#region Vars

		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestRepository>();
		var repositoryId = repository.Setup(x => x.GetByIdAsync(buyRequest.Id)).ReturnsAsync(buyRequest);
		repository.Setup(x => x.UpdateAsync(buyRequest)).ReturnsAsync(buyRequest);

		var service = _mocker.CreateInstance<BuyRequestService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(buyRequest);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<BuyRequests>()), Times.Once);
	}

	[Fact]
	public async Task BuyRequestService_Patch()
	{
		//Arrange

		#region Vars

		var buyRequestFaker = new BuyRequestFaker();
		var buyRequest = buyRequestFaker.buyRequest;

		var repository = _mocker.GetMock<IBuyRequestRepository>();
		repository.Setup(x => x.GetByIdAsync(buyRequest.Id)).ReturnsAsync(buyRequest);
		repository.Setup(x => x.PatchAsync(buyRequest));

		var service = _mocker.CreateInstance<BuyRequestService>();

		#endregion Vars

		//Act
		await service.PatchAsync(buyRequest);

		//Assert
		repository.Verify(x => x.PatchAsync(It.IsAny<BuyRequests>()), Times.Once);
	}

	private Mock<IOptions<RabbitMqOptions>> Options()
	{
		var mockoptions = _mocker.GetMock<IOptions<RabbitMqOptions>>();
		return mockoptions;
	}
}
