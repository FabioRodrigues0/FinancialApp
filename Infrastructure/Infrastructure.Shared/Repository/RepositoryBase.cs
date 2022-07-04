using Infrastructure.Shared.Data;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Shared.Repository
{
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

		public virtual async Task<PagesBase<T>> GetAllAsync(int page, int itemsPerPage)
		{
			var query = dbSet
				.AsNoTracking()
				.Skip((page - 1) * dbSet.Count())
				.Take(itemsPerPage);

			if (Include != null)
				query = Include(query);
			_logger.LogInformation("{time} [INFO] - Calls a List {T}", DateTime.Now, query.GetType());
			var result = await query.ToListAsync();
			var totalPages = (int)Math.Ceiling(dbSet.Count() / (float)itemsPerPage);
			return ConvertToPages(result, totalPages, page);
		}

		public async Task<PagesBase<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int page, int itemsPerPage)
		{
			var query = dbSet.Where(predicate)
										.Skip((page - 1) * dbSet.Count())
										.Take(itemsPerPage);
			var result = await query.ToListAsync();
			var totalPages = (int)Math.Ceiling(result.Count() / (float)itemsPerPage);
			return ConvertToPages(result, totalPages, page);
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
		{
			var query = dbSet.Where(predicate);
			var result = await query.ToListAsync();
			return result;
		}

		public virtual async Task<T> GetByIdAsync(Guid id)
		{
			var query = dbSet
				.Where(br => br.Id == id)
				.AsNoTracking();

			if (Include != null)
				query = Include(query);

			_logger.LogInformation("{time} [INFO] - Call a {T} with Id = {id}", DateTime.Now, query.GetType(), id);
			return await query.FirstOrDefaultAsync();
		}

		public virtual async Task<bool> RemoveAsync(Guid id)
		{
			var obj = await dbSet
				.Where(x => x.Id == id)
				.AsNoTracking()
				.FirstOrDefaultAsync();
			_logger.LogInformation("{time} [INFO] - Call a Request to Delete with Id = {id}", DateTime.Now, id);
			var result = dbSet.Remove(obj);
			await _dataContext.SaveChangesAsync();
			return result != null;
		}

		public virtual async Task<bool> RemoveAsync(T obj)
		{
			_logger.LogInformation("{time} [INFO] - Call a Request to Delete {obj}", DateTime.Now, obj);
			var result = dbSet.Remove(obj);
			await _dataContext.SaveChangesAsync();
			return result != null;
		}

		public virtual async Task<T> UpdateAsync(T obj)
		{
			_logger.LogInformation("{time} [INFO] - Call Update on {obj}", DateTime.Now, obj);
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

		public virtual PagesBase<T> ConvertToPages(List<T> list, int page, int totalPages)
		{
			return new PagesBase<T> { Models = list, CurrentPage = totalPages, Pages = page };
		}
	}
}
