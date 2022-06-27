using Infrastructure.Shared.Models;

namespace Infrastructure.Shared.Services.Interface
{
	public interface IServiceBase<T> where T : EntityBase<T>
	{
		Task<T> AddAsync(T obj);

		Task<T> UpdateAsync(T obj);

		Task<bool> RemoveAsync(Guid id);

		Task<T> PatchAsync(T obj);

		Task<(List<T> list, int totalPages, int page)> GetAllAsync(int page);

		Task<T> GetByIdAsync(Guid id);

		Task<bool> ValidateEntity(T domain);

		void NoContent(bool content);

		void AddNotification(string message);
	}
}
