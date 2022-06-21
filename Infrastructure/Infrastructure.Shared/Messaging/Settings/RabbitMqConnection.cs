using Infrastructure.Shared.Messaging.Settings.Interface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Shared.Messaging.Settings
{
	public class RabbitMQConnection : IRabbitMQConnectionFactory
	{
		private readonly RabbitMqOptions connectionDetails;

		public RabbitMQConnection(IOptions<RabbitMqOptions> connectionDetails)
		{
			this.connectionDetails = connectionDetails.Value;
		}

		public IConnection CreateConnection()
		{
			var factory = new ConnectionFactory {
				HostName = connectionDetails.StringConnection
			};
			var connection = factory.CreateConnection();
			return connection;
		}
	}
}
