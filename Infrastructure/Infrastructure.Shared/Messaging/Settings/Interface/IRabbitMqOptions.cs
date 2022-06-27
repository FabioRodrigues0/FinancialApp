namespace Infrastructure.Shared.Messaging.Settings.Interface
{
	public interface IRabbitMqOptions
	{
		string GetRabbitMQ();

		string GetQueueName();

		string GetTopic();

		string GetRoutingKey();
	}
}
