namespace Infrastructure.Shared.Messaging.Publisher.Interface
{
	public interface IRabbitMqPublisher<T> where T : class
	{
		void SendToRabbit(T obj, string queueName);
	}
}
