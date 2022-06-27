namespace Infrastructure.Shared.Messaging.Settings
{
	public class RabbitMqOptions
	{
		private string _stringConnection;

		public string StringConnection {
			get {
				return _stringConnection ?? throw new InvalidOperationException("Rabbitmq Base Address must be setted.");
			}
			set { _stringConnection = value; }
		}

		private string _queueName;

		public string QueueName {
			get {
				return _queueName ?? throw new InvalidOperationException("Rabbitmq Queue Name must be setted.");
			}
			set { _queueName = value; }
		}

		private string _queueNameMP;

		public string QueueNameMP {
			get {
				return _queueNameMP ?? throw new InvalidOperationException("Rabbitmq Queue Name must be setted.");
			}
			set { _queueNameMP = value; }
		}

		private string _topic;

		public string Topic {
			get {
				return _topic ?? throw new InvalidOperationException("Rabbitmq Topic must be setted.");
			}
			set { _topic = value; }
		}

		private string _routingKey;

		public string RoutingKey {
			get {
				return _routingKey ?? throw new InvalidOperationException("Rabbitmq Routing Key must be setted.");
			}
			set { _routingKey = value; }
		}

		public string GetRabbitMQ() => $"{StringConnection}";

		public string GetQueueName() => $"{QueueName}";

		public string GetQueueNameMP() => $"{QueueNameMP}";

		public string GetTopic() => $"{Topic}";

		public string GetRoutingKey() => $"{RoutingKey}";
	}
}
