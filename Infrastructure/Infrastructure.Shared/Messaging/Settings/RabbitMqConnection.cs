using Infrastructure.Shared.Messaging.Settings.Interface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Shared.Messaging.Settings
{
	public class RabbitMQConnection : IRabbitMQConnectionFactory
	{
		private readonly RabbitMqOptions _connectionDetails;

		public RabbitMQConnection(IOptions<RabbitMqOptions> connectionDetails)
		{
			_connectionDetails = connectionDetails.Value;
		}

		public IConnection CreateConnection()
		{
			var factory = new ConnectionFactory {
				HostName = _connectionDetails.GetRabbitMQ()
			};
			var connection = factory.CreateConnection();
			return connection;
		}
	}
}
