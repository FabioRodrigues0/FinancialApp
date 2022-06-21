using Infrastructure.Shared.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Shared.Controller
{
	[ApiController]
	public class ApiControllerBase : ControllerBase
	{
		private readonly IServiceContext _serviceContext;

		public ApiControllerBase(IServiceContext serviceContext)
		{
			_serviceContext = serviceContext;
		}

		/// <summary>
		/// Response default
		/// </summary>
		/// <param name="result"></param>
		protected IActionResult ServiceResponse(object result = null)
		{
			return _serviceContext.HasNotification()
				? BadRequest(new ApiResult<string>(_serviceContext.Notifications))
				: _serviceContext.HasContent()
				? Ok(new ApiResult<object>(result))
				: NoContent();
		}

		/// <summary>
		/// Response default
		/// </summary>
		/// <param name="result"></param>
		protected IActionResult ServiceResponse<T>(T result = default)
		{
			return _serviceContext.HasNotification()
				? BadRequest(new ApiResult<string>(_serviceContext.Notifications))
				: _serviceContext.HasContent()
				? Ok(new ApiResult<T>(result))
				: NoContent();
		}
	}
}
