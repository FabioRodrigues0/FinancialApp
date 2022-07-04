using FluentValidation.Results;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Shared.Services
{
	public class ValidationsBase<T> : IValidationsBase<T> where T : EntityBase<T>
	{
		protected readonly IServiceContext _serviceContext;
		private readonly ILogger<ValidationsBase<T>> _logger;

		protected ValidationsBase(
			ILogger<ValidationsBase<T>> logger,
			IServiceContext serviceContext)
		{
			_logger = logger;
			_serviceContext = serviceContext;
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
