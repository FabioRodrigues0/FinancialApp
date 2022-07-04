using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Messaging.Settings.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infrastructure.Shared.Messaging.Listener
{
	public class RabbitMqListener : IHostedService
	{
		#region Vars

		private readonly ILogger<RabbitMqListener> _logger;
		private readonly IRabbitMQConnectionFactory _factory;
		private readonly RabbitMqOptions _options;
		private readonly IModel _channel;
		private readonly string queueName;
		private readonly string connectionString;
		private readonly string topic;
		private readonly string routingKey;
		private IConnection _connection;
		private EventingBasicConsumer _consumer;

		#endregion Vars

		public RabbitMqListener(
			ILogger<RabbitMqListener> logger,
			IOptions<RabbitMqOptions> options,
			IRabbitMQConnectionFactory factory)
		{
			_logger = logger;
			_factory = factory;
			_options = options.Value;

			#region Set strings

			connectionString = _options.GetRabbitMQ();
			queueName = _options.GetQueueName();
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

		public async Task ReceivedFromRabbit()
		{
			_channel.ExchangeDeclare(exchange: queueName, type: topic);
			_channel.QueueDeclare(queue: queueName,
											 durable: false,
											 exclusive: false,
											 autoDelete: false,
											 arguments: null);

			_channel.QueueBind(queue: queueName, exchange: queueName, routingKey: routingKey);

			_logger.LogInformation("[INFO] - Waiting for messages");

			_consumer = new EventingBasicConsumer(_channel);
			_consumer.Received += async (model, ea) =>
			{
				try
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					await SendToApplication(message);
					_channel.BasicAck(ea.DeliveryTag, false);
				}
				catch (Exception ex)
				{
					_channel.BasicNack(ea.DeliveryTag, false, true);
				}
			};
			_channel.BasicConsume(queue: queueName, autoAck: false, consumer: _consumer);
		}

		public virtual Task<bool> SendToApplication(string message)
		{
			throw new NotImplementedException();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await ReceivedFromRabbit();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_connection.Close();
			return Task.CompletedTask;
		}
	}
}
