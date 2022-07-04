using Infrastructure.Shared.Entities;

namespace Infrastructure.Shared.Services.Interface
{
	public interface IServiceBase<T> where T : EntityBase<T>
	{
		Task<T> AddAsync(T obj);

		Task<T> UpdateAsync(T obj);

		Task<bool> RemoveAsync(Guid id);

		Task<T> PatchAsync(T obj);

		Task<PagesBase<T>> GetAllAsync(int page, int itemsPerPage);

		Task<T> GetByIdAsync(Guid id);

		Task<bool> ValidateEntity(T domain);

		void NoContent(bool content);

		void AddNotification(string message);
	}
}
