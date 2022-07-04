using CashBook.Domain.Entities;
using Infrastructure.Shared.Repository.Interface;

namespace CashBook.Data.Repositories.Interfaces;

public interface ICashBookRepository : IRepositoryBase<CashBooks>
{
	Task<List<CashBooks>> GetByOriginId(Guid id);
}