using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Messaging.Settings.Interface;
using MessageBroker.Consumer;
using MessageBroker.Publisher;
using MessageBroker.Publisher.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.Settings.Queue;

public static class QueueExtentions
{
	public static void AddConsumer(this IServiceCollection services)
	{
		services.AddHostedService<CashBookConsumer>();
		services.AddScoped<RabbitMqOptions, RabbitMqOptions>();
	}

	public static void AddPublisher(this IServiceCollection services)
	{
		services.AddSingleton<ICashBookPublisher, CashBookPublisher>();
		services.AddSingleton<IMovementsPublisher, MovementsPublisher>();
		services.AddSingleton<IRabbitMQConnectionFactory, RabbitMQConnection>();
	}
}
