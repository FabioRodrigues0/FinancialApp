using Infrastructure.Shared.Entities;

namespace Infrastructure.Shared.Services.Interface
{
	public interface IValidationsBase<T> where T : EntityBase<T>
	{
		Task<bool> ValidateEntity(T domain);

		void NoContent(bool content);

		void AddNotification(string message);
	}
}
