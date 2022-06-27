using CashBook.Domain.Models;
using Infrastructure.Shared.Services.Interface;

namespace CashBook.Application.Services.Interface;

public interface ICashBookService : IServiceBase<CashBooks>
{
	Task<List<CashBooks>> GetByOriginIdAsync(Guid id);
}
