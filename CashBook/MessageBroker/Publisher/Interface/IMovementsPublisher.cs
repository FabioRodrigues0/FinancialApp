using CashBook.Application.DTO;
using Infrastructure.Shared.Messaging.Publisher.Interface;
using Stock.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Publisher.Interface
{
	public interface IMovementsPublisher : IRabbitMqPublisher<MovementsDto>
	{
	}
}
