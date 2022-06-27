using CashBook.Application.DTO;
using Infrastructure.Shared.Messaging.Publisher;
using Infrastructure.Shared.Messaging.Settings.Interface;
using Infrastructure.Shared.Messaging.Settings;
using MessageBroker.Publisher.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Application.DTO;

namespace MessageBroker.Publisher
{
	public class MovementsPublisher : RabbitMqPublisher<MovementsDto>, IMovementsPublisher
	{
		public MovementsPublisher(
			IOptions<RabbitMqOptions> options,
			ILogger<RabbitMqPublisher<MovementsDto>> logger,
			IRabbitMQConnectionFactory factory) : base(options, logger, factory)
		{
		}
	}
}
