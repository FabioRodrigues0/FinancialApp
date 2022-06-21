using RabbitMQ.Client;

namespace Infrastructure.Shared.Messaging.Settings.Interface
{
	public interface IRabbitMQConnectionFactory
	{
		IConnection CreateConnection();
	}
}
