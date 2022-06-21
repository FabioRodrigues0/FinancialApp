using Infrastructure.Shared.Messaging.Publisher.Interface;
using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Messaging.Settings.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Shared.Messaging.Publisher
{
	public class RabbitMqPublisher<T> : IRabbitMqPublisher<T> where T : class
	{
		private readonly string queueName;
		private readonly string queueNameMP;
		private readonly string connectionString;
		private readonly string topic;
		private readonly string routingKey;
		private readonly IRabbitMQConnectionFactory _factory;
		private readonly RabbitMqOptions _options;
		private readonly ILogger<RabbitMqPublisher<T>> _logger;
		private readonly IModel _channel;
		private IConnection _connection;

		public RabbitMqPublisher(
			IOptions<RabbitMqOptions> options,
			ILogger<RabbitMqPublisher<T>> logger,
			IRabbitMQConnectionFactory factory)
		{
			_logger = logger;
			_factory = factory;
			_options = options.Value;

			#region Set strings
			queueName = _options.GetQueueName();
			queueNameMP = _options.GetQueueNameMP();
			connectionString = _options.GetRabbitMQ();
			topic = _options.GetTopic();
			routingKey = _options.GetRoutingKey();

			#endregion Set strings

			try
			{
				_connection = _factory.CreateConnection();
				_channel = _connection.CreateModel();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"RabbitListener init error,ex:{ex.Message}");
			}
		}

		public virtual void SendToRabbit(T obj, string queueName)
		{
			_channel.ExchangeDeclare(exchange: queueName, type: topic);
			_channel.QueueDeclare(queue: queueName,
								 durable: false,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);
			_channel.QueueBind(queue: queueName,
								exchange: queueName,
								routingKey: routingKey);

			var message = JsonConvert.SerializeObject(obj);
			var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

			_channel.BasicPublish(exchange: queueName,
								 routingKey: routingKey,
								 basicProperties: null,
								 body: body);

			_logger.LogInformation(" - Sent {0}", message);
		}
	}
}
