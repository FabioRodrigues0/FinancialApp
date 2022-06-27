using CashBook.Data.Repositories.Interfaces;
using CashBook.Domain.Models;
using Infrastructure.Shared.Repository;
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

	public override async Task<CashBooks> AddAsync(CashBooks obj)
	{
		try
		{
			_logger.LogInformation("{time} [INFO] - Create new {T}", DateTime.Now, obj.GetType());
			await dbSet.AddAsync(obj);
			await _dataContext.SaveChangesAsync();
		}
		catch (Exception e)
		{
			_logger.LogWarning("{time} [WARN] - Exception {e}", DateTime.Now, e);
		}
		return obj;
	}

	public CashBooks GetById(Guid id)
	{
		_logger.LogInformation("{time} [INFO] - Call a CashBook with Id = {id}", DateTime.Now, id);
		return dbSet.Where(c => c.Id == id).FirstOrDefault();
	}

	public override async Task<CashBooks> UpdateAsync(CashBooks obj)
	{
		_logger.LogInformation("{time} [INFO] - Call Update on {obj}", DateTime.Now, obj);
		return await base.UpdateAsync(obj);
	}

	public async Task<List<CashBooks>> GetByOriginId(Guid id)
	{
		_logger.LogInformation("{time} [INFO] - Calls CashBooks with OriginId {id}", DateTime.Now, id);
		var result = await dbSet
			.AsNoTracking()
			.ToListAsync();
		result.Select(c => c.OriginId == id);
		return result;
	}
}
