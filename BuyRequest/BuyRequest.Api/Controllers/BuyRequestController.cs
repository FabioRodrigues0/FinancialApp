using BuyRequest.Application.Application.Interface;
using BuyRequest.Application.DTO;
using Infrastructure.Shared.Controller;
using Infrastructure.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BuyRequest.Api.Controllers;

[Route("api/[controller]")]
public class BuyRequestController : ApiControllerBase
{
	private readonly ILogger<BuyRequestController> _logger;
	private readonly IApplicationBuyRequestService _applicationBuyRequestService;

	public BuyRequestController(
		IApplicationBuyRequestService applicationBuyRequestService,
		ILogger<BuyRequestController> logger,
		IServiceContext context) : base(context)
	{
		_logger = logger;
		_applicationBuyRequestService = applicationBuyRequestService;
	}

	/// <summary>
	/// Calls Page(int) of 10 BuyRequests in Database
	/// </summary>
	/// <returns>Page(int) of 10 BuyRequest in Data base</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have any BuyRequest in Database</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("page/{page}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get([FromRoute] int page)
	{
		_logger.LogInformation("Begin Request for BuyRequests {page}", page);
		return ServiceResponse(await _applicationBuyRequestService.GetAllAsync(page));
	}

	/// <summary>
	/// Gets BuyRequest in Database with ID indicated
	/// </summary>
	/// <returns>BuyRequest in Database with ID indicated</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a BuyRequest with ID indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get([FromRoute] Guid id)
	{
		_logger.LogInformation("Begin Request for BuyRequests with Id = {id}", id);
		return ServiceResponse(await _applicationBuyRequestService.GetByIdAsync(id));
	}

	/// <summary>
	/// Gets BuyRequest in Database with by ClientId
	/// </summary>
	/// <returns>BuyRequest in Database by ClientId</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a BuyRequest with ClientId indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("client/{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> GetByClientId([FromRoute] Guid id)
	{
		_logger.LogInformation("Begin Request for BuyRequests with ClientId = {id}", id);
		return ServiceResponse(await _applicationBuyRequestService.GetByClientIdAsync(id));
	}

	/// <summary>
	/// Send a BuyRequest
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the BuyRequest inserted
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPost]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Post([FromBody] BuyRequestDto obj)
	{
		_logger.LogInformation("Begin Request for Create a BuyRequests({obj})", obj);
		return ServiceResponse(await _applicationBuyRequestService.AddAsync(obj));
	}

	/// <summary>
	/// Send a Update to BuyRequest
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the BuyRequest updated
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Put([FromBody] BuyRequestUpdateDto obj)
	{
		_logger.LogInformation("Begin Request for Update a BuyRequests({obj})", obj);
		return ServiceResponse(await _applicationBuyRequestService.UpdateAsync(obj));
	}

	/// <summary>
	/// Send a Update to Status of BuyRequest
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return null and the BuyRequest updated
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPatch]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Patch([FromBody] BuyRequestPatchDto obj)
	{
		_logger.LogInformation("Begin Request for Update Status from {obj}", obj);
		return ServiceResponse(await _applicationBuyRequestService.PatchAsync(obj));
	}

	/// <summary>
	/// Send a Request to Delete a BuyRequest
	/// </summary>
	/// <returns>BuyRequest Deleted</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have any BuyRequest with Id indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpDelete("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete([FromRoute] Guid id)
	{
		_logger.LogInformation("Begin Request for Delete BuyRequests with Id = {id}", id);
		return ServiceResponse(await _applicationBuyRequestService.RemoveAsync(id));
	}
}