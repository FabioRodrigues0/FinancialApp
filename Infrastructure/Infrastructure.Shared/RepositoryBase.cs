using System.Collections.Generic;
using Infrastructure.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Shared;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase<T>
{
	protected readonly DataContext _dataContext;
	protected readonly DbSet<T> dbSet;
	private readonly ILogger<RepositoryBase<T>> _logger;
	protected Func<IQueryable<T>, IIncludableQueryable<T, object>> Include;

	public RepositoryBase(
		ILogger<RepositoryBase<T>> logger,
		DataContext dataContext)
	{
		_logger = logger;
		_dataContext = dataContext;
		dbSet = _dataContext.Set<T>();
	}

	public virtual void SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
	{
		Include = include;
	}

	public virtual async Task<T> AddAsync(T obj)
	{
		try
		{
			_logger.LogInformation("Create new {T}", obj.GetType());
			await dbSet.AddAsync(obj);
			await _dataContext.SaveChangesAsync();
		}
		catch (Exception e)
		{
			_logger.LogWarning("Exception {e}", e);
			throw;
		}
		return obj;
	}

	public virtual async Task<(List<T> list, int totalPages, int page)> GetAllAsync(int page)
	{
		const int pageResults = 10;

		var query = dbSet
			.AsNoTracking()
			.Skip((page - 1) * dbSet.Count())
			.Take(pageResults);

		if (Include != null)
			query = Include(query);
		_logger.LogInformation("Calls a List {T}", query.GetType());
		var result = await query.ToListAsync();
		var totalPages = (int)Math.Ceiling(dbSet.Count() / 10f);
		return (result, totalPages, page);
	}

	public virtual async Task<T> GetByIdAsync(Guid id)
	{
		var query = dbSet
			.Where(br => br.Id == id)
			.AsNoTracking();

		if (Include != null)
			query = Include(query);

		_logger.LogInformation("Call a {T} with Id = {id}", query.GetType(), id);
		return await query.FirstOrDefaultAsync();
	}

	public virtual async Task<bool> RemoveAsync(Guid id)
	{
		var obj = await dbSet
			.Where(x => x.Id == id)
			.AsNoTracking()
			.FirstOrDefaultAsync();
		_logger.LogInformation("Call a Request to Delete with Id = {id}", id);
		var result = dbSet.Remove(obj);
		await _dataContext.SaveChangesAsync();
		return result != null;
	}

	public virtual async Task<bool> RemoveAsync(T obj)
	{
		_logger.LogInformation("Call a Request to Delete {obj}", obj);
		var result = dbSet.Remove(obj);
		await _dataContext.SaveChangesAsync();
		return result != null;
	}

	public virtual async Task<T> UpdateAsync(T obj)
	{
		_logger.LogInformation("Call Update on {obj}", obj);
		dbSet.Update(obj);
		await _dataContext.SaveChangesAsync();
		return obj;
	}

	public virtual async Task<T> PatchAsync(T obj)
	{
		dbSet.Update(obj);
		await _dataContext.SaveChangesAsync();
		return obj;
	}
}