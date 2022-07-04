using CashBook.Application.Models;
using CashBook.Domain.Entities;

namespace CashBook.Application.Application.Interface;

public interface IApplicationCashBookService
{
	Task<CashBooks> AddAsync(CashBookModel obj);

	Task<CashBookModel> GetByIdAsync(Guid id);

	Task<List<CashBookModel>> GetByOriginIdAsync(Guid id);

	Task<PagesCashBookModel> GetAllAsync(int page, int itemsPerPage);

	Task<CashBooks> UpdateAsync(CashBookUpdateModel obj);
}
