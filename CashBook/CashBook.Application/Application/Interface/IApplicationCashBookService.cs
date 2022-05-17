using CashBook.Application.DTO;
using CashBook.Domain.Models;

namespace CashBook.Application.Application.Interface;

public interface IApplicationCashBookService
{
	Task<CashBooks> AddAsync(CashBookDto obj);

	Task<CashBookDto> GetByIdAsync(Guid id);

	Task<List<CashBookDto>> GetByOriginIdAsync(Guid id);

	Task<PagesCashBookDto> GetAllAsync(int page);

	Task<CashBooks> UpdateAsync(CashBookUpdateDto obj);
}