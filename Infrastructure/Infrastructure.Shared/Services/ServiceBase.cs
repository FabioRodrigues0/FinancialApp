using FluentValidation.Results;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Repository.Interface;
using Infrastructure.Shared.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Shared.Services
{
	public abstract class ServiceBase<T> : IServiceBase<T> where T : EntityBase<T>
	{
		private readonly IRepositoryBase<T> _tEntityRepository;
		protected readonly IServiceContext _serviceContext;
		private readonly ILogger<ServiceBase<T>> _logger;

		protected ServiceBase(
			ILogger<ServiceBase<T>> logger,
			IRepositoryBase<T> tEntityRepository,
			IServiceContext serviceContext)
		{
			_logger = logger;
			_serviceContext = serviceContext;
			_tEntityRepository = tEntityRepository;
		}

		public virtual async Task<T> AddAsync(T obj)
		{
			return await _tEntityRepository.AddAsync(obj);
		}

		public virtual async Task<T> GetByIdAsync(Guid id)
		{
			return await _tEntityRepository.GetByIdAsync(id);
		}

		public virtual async Task<PagesBase<T>> GetAllAsync(int page, int itemsPerPage)
		{
			return await _tEntityRepository.GetAllAsync(page, itemsPerPage);
		}

		public virtual async Task<T> UpdateAsync(T obj)
		{
			return await _tEntityRepository.UpdateAsync(obj);
		}

		public virtual Task<T> PatchAsync(T obj)
		{
			return _tEntityRepository.PatchAsync(obj);
		}

		public virtual async Task<bool> RemoveAsync(Guid id)
		{
			return await _tEntityRepository.RemoveAsync(id);
		}

		public async Task<bool> ValidateEntity(T domain)
		{
			await domain.IsValid();
			if (domain?.ValidationResult?.Errors.Any() == true)
				foreach (var domainErro in domain.ValidationResult.Errors)
					AddNotification(domainErro);

			return IsValidOperation;
		}

		private void AddNotification(ValidationFailure error)
		{
			_logger.LogError(error.ToString());
			AddNotification(error.ErrorMessage);
		}

		public void NoContent(bool content) => _serviceContext.NoContent(content);

		public void AddNotification(string message) => _serviceContext.AddNotification(message);

		public bool IsValidOperation => !_serviceContext.HasNotification();
	}
}
