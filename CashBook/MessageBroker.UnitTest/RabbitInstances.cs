using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Messaging.Settings.Interface;
using MessageBroker.Consumer;
using MessageBroker.Publisher;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using RabbitMQ.Client;

namespace MessageBroker.UnitTest;

public abstract class RabbitInstances
{
	public readonly AutoMocker _mocker;
	private readonly string queueName;
	private readonly string connectionString;
	private readonly string topic;
	private readonly string routingKey;
	private readonly RabbitMqOptions _appSettings;

	public RabbitInstances()
	{
		_mocker = new AutoMocker();
		queueName = "queueTest";
		connectionString = "localhost";
		topic = "topicTest";
		routingKey = "routingKeyTest";
		_appSettings = new RabbitMqOptions() {
			QueueName = queueName,
			StringConnection = connectionString,
			Topic = topic,
			RoutingKey = routingKey,
		};
	}

	public CashBookPublisher CreatePublisherInstance()
	{
		var mockoptions = Options();
		mockoptions.Setup(ap => ap.Value).Returns(_appSettings);

		//Mock Connection
		var _factory = _mocker.GetMock<IRabbitMQConnectionFactory>();
		var _connection = _mocker.GetMock<IConnection>();
		var _channel = _mocker.GetMock<IModel>();

		_factory.Setup(x => x.CreateConnection()).Returns(_connection.Object);
		_connection.Setup(x => x.CreateModel()).Returns(_channel.Object);

		//Mock Instances
		var publisher = _mocker.CreateInstance<CashBookPublisher>();
		return publisher;
	}

	public CashBookConsumer CreateConsumerInstance()
	{
		var mockoptions = Options();
		mockoptions.Setup(ap => ap.Value).Returns(_appSettings);

		//Mock Connection
		var _factory = _mocker.GetMock<IRabbitMQConnectionFactory>();
		var _connection = _mocker.GetMock<IConnection>();
		var _channel = _mocker.GetMock<IModel>();

		_factory.Setup(x => x.CreateConnection()).Returns(_connection.Object);
		_connection.Setup(x => x.CreateModel()).Returns(_channel.Object);

		//Mock Instances
		var consumer = _mocker.CreateInstance<CashBookConsumer>();
		return consumer;
	}

	private Mock<IOptions<RabbitMqOptions>> Options()
	{
		var mockoptions = _mocker.GetMock<IOptions<RabbitMqOptions>>();
		return mockoptions;
	}
}
