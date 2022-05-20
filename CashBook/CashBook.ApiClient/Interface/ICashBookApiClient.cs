using CashBook.Application.DTO;
using Infrastructure.Shared;

namespace CashBook.ApiClient.Interface;

public interface ICashBookApiClient
{
	void SendToRabbit(CashBookDto obj);
}