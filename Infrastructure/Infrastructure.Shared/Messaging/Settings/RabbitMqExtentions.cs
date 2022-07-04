using Infrastructure.Shared.Messaging.Listener;
using Infrastructure.Shared.Messaging.Settings.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared.Messaging.Settings
{
	public static class RabbitMqExtentions
	{
		public static void AddListener(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<RabbitMqOptions>(options =>
			{
				options.StringConnection = configuration["RabbitmqBaseSettings:StringConnection"];
				options.QueueName = configuration["RabbitmqBaseSettings:QueueName"];
				options.QueueNameMP = configuration["RabbitmqBaseSettings:QueueNameMP"];
				options.Topic = configuration["RabbitmqBaseSettings:Topic"];
				options.RoutingKey = configuration["RabbitmqBaseSettings:RoutingKey"];
			});

			services.AddSingleton<RabbitMqListener, RabbitMqListener>();
		}
	}
}
