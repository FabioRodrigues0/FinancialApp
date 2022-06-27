using Document.Application.Application.Interface;
using Document.Application.DTO;
using Infrastructure.Shared.Controller;
using Infrastructure.Shared.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Document.Api.Controllers;

[Route("api/[controller]")]
public class DocumentController : ApiControllerBase
{
	private readonly ILogger<DocumentController> _logger;
	private readonly IApplicationDocumentService _applicationDocumentService;

	public DocumentController(
		IApplicationDocumentService applicationDocumentService,
		ILogger<DocumentController> logger,
		IServiceContext context) : base(context)
	{
		_logger = logger;
		_applicationDocumentService = applicationDocumentService;
	}

	/// <summary>
	/// Calls Page(int) of 10 Documents in Database
	/// </summary>
	/// <returns>Page(int) of 10 Documents in Data base</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have any Document in Database</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("page/{page}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get([FromRoute] int page)
	{
		_logger.LogInformation("Begin Request for Documents {page}", page);
		return ServiceResponse(await _applicationDocumentService.GetAllAsync(page));
	}

	/// <summary>
	/// Gets Document in Database with ID indicated
	/// </summary>
	/// <returns>Document in Data base with ID indicated</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a Document with ID indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get([FromRoute] Guid id)
	{
		_logger.LogInformation("Begin Request for Documents with Id = {id}", id);
		return ServiceResponse(await _applicationDocumentService.GetByIdAsync(id));
	}

	/// <summary>
	/// Send a Document
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the Document inserted
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPost]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Post([FromBody] DocumentDto obj)
	{
		_logger.LogInformation("Begin Request for Create a Document({obj})", obj);
		return ServiceResponse(await _applicationDocumentService.AddAsync(obj));
	}

	/// <summary>
	/// Send a Update to Document
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the Document updated
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Put([FromBody] DocumentUpdateDto obj)
	{
		_logger.LogInformation("Begin Request for Update a Document({obj})", obj);
		return ServiceResponse(await _applicationDocumentService.UpdateAsync(obj));
	}

	/// <summary>
	/// Send a Request to Delete a Document
	/// </summary>
	/// <returns>Document Deleted</returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpDelete("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete([FromRoute] Guid id)
	{
		_logger.LogInformation("Begin Request for Delete Document with Id = {id}", id);
		return ServiceResponse(await _applicationDocumentService.RemoveAsync(id));
	}

	/// <summary>
	/// Send a Update to Paid of Document
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
	public async Task<IActionResult> Patch([FromBody] DocumentPatchDto obj)
	{
		_logger.LogInformation("Begin Request for Update Paid from {obj}", obj);
		return ServiceResponse(await _applicationDocumentService.PatchAsync(obj));
	}
}