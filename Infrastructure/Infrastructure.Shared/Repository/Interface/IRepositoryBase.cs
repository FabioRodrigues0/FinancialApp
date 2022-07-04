using Infrastructure.Shared.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Shared.Repository.Interface
{
	public interface IRepositoryBase<T> where T : EntityBase<T>
	{
		void SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

		Task<T> AddAsync(T obj);

		Task<T> GetByIdAsync(Guid id);

		Task<PagesBase<T>> GetAllAsync(int page, int itemsPerPage);

		Task<PagesBase<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int page, int itemsPerPage);

		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

		Task<T> UpdateAsync(T obj);

		Task<bool> RemoveAsync(Guid id);

		Task<bool> RemoveAsync(T obj);

		Task<T> PatchAsync(T obj);

		PagesBase<T> ConvertToPages(List<T> list, int page, int totalPages);
	}
}
