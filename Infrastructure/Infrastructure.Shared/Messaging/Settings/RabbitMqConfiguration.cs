using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared.Messaging.Settings
{
	public static class RabbitMqConfiguration
	{
		public static void AddListenerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<RabbitMqOptions>(options =>
			{
				options.StringConnection = configuration["RabbitmqBaseSettings:StringConnection"];
				options.QueueNameMP = configuration["RabbitmqBaseSettings:QueueNameMP"];
				options.QueueName = configuration["RabbitmqBaseSettings:QueueName"];
				options.Topic = configuration["RabbitmqBaseSettings:Topic"];
				options.RoutingKey = configuration["RabbitmqBaseSettings:RoutingKey"];
			});
		}
	}
}
