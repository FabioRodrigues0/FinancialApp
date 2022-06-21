using CashBook.Application.DTO;
using Infrastructure.Shared.Messaging.Publisher.Interface;

namespace MessageBroker.Publisher.Interface;

public interface ICashBookPublisher : IRabbitMqPublisher<CashBookDto>
{
}
