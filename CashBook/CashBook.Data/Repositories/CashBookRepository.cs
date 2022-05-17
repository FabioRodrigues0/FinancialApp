using CashBook.Data.Repositories.Interfaces;
using CashBook.Domain.Models;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashBook.Data.Repositories;

public class CashBookRepository : RepositoryBase<CashBooks>, ICashBookRepository
{
	private readonly ILogger<CashBookRepository> _logger;

	public CashBookRepository(
		CashBookContext context,
		ILogger<CashBookRepository> logger
		) : base(logger, context)
	{
		_logger = logger;
	}

	public CashBooks GetById(Guid id)
	{
		_logger.LogInformation("Call a CashBook with Id = {id}", id);
		return dbSet.Where(c => c.Id == id).FirstOrDefault();
	}

	public override async Task<CashBooks> UpdateAsync(CashBooks obj)
	{
		_logger.LogInformation("Call Update on {obj}", obj);
		return await base.UpdateAsync(obj);
	}

	public async Task<List<CashBooks>> GetByOriginId(Guid id)
	{
		_logger.LogInformation("Calls CashBooks with OriginId {id}", id);
		var result = await dbSet
			.AsNoTracking()
			.ToListAsync();
		result.Select(c => c.OriginId == id);
		return result;
	}
}