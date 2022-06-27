using Infrastructure.Shared.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Shared.Repository.Interface
{
	public interface IRepositoryBase<T> where T : EntityBase<T>
	{
		void SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

		Task<T> AddAsync(T obj);

		Task<T> GetByIdAsync(Guid id);

		Task<(List<T> list, int totalPages, int page)> GetAllAsync(int page);

		Task<(List<T> list, int totalPages, int page)> GetAllAsync(Expression<Func<T, bool>> predicate, int page);

		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

		Task<T> UpdateAsync(T obj);

		Task<bool> RemoveAsync(Guid id);

		Task<bool> RemoveAsync(T obj);

		Task<T> PatchAsync(T obj);
	}
}
