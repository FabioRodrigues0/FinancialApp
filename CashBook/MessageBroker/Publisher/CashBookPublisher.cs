using CashBook.Application.DTO;
using Infrastructure.Shared.Messaging.Publisher;
using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Messaging.Settings.Interface;
using MessageBroker.Publisher.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MessageBroker.Publisher;

public class CashBookPublisher : RabbitMqPublisher<CashBookDto>, ICashBookPublisher
{
	public CashBookPublisher(
		IOptions<RabbitMqOptions> options,
		ILogger<RabbitMqPublisher<CashBookDto>> logger,
		IRabbitMQConnectionFactory factory) : base(options, logger, factory)
	{
	}
}
