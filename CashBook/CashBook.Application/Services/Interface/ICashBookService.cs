using CashBook.Application.DTO;
using CashBook.Domain.Models;
using Infrastructure.Shared.Interfaces;

namespace CashBook.Application.Services.Interface;

public interface ICashBookService : IServiceBase<CashBooks>
{
	Task<List<CashBooks>> GetByOriginIdAsync(Guid id);
}