using CashBook.Application.Models;
using Infrastructure.Shared.Messaging.Publisher.Interface;

namespace MessageBroker.Publisher.Interface;

public interface ICashBookPublisher : IRabbitMqPublisher<CashBookModel>
{
}
