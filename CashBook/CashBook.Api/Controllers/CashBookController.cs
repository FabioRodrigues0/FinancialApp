using CashBook.Application.Application.Interface;
using CashBook.Application.DTO;
using Infrastructure.Shared.Controller;
using Infrastructure.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashBook.Api.Controllers;

[Route("api/[controller]")]
public class CashBookController : ApiControllerBase
{
	private readonly ILogger<CashBookController> _logger;
	private readonly IApplicationCashBookService _applicationCashBookService;

	public CashBookController(
		IApplicationCashBookService applicationCashBookService,
		ILogger<CashBookController> logger,
		IServiceContext context) : base(context)
	{
		_logger = logger;
		_applicationCashBookService = applicationCashBookService;
	}

	/// <summary>
	/// Calls 10 CashBook in Data base
	/// </summary>
	/// <returns>10 CashBook in Database</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have any CashBook in Database</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("page/{page}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get(int page)
	{
		_logger.LogInformation("Begin Request for CashBooks {page}", page);
		return ServiceResponse(await _applicationCashBookService.GetAll(page));
	}

	/// <summary>
	/// Gets CashBook in Database with ID indicated
	/// </summary>
	/// <returns>CashBook in Database with ID indicated</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a CashBook with ID indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get(Guid id)
	{
		_logger.LogInformation("Begin Request for CashBook with Id = {id}", id);
		return ServiceResponse(await _applicationCashBookService.GetById(id));
	}

	/// <summary>
	/// Gets CashBook in Database with OriginID indicated
	/// </summary>
	/// <returns>BuyRequest in Data base with OriginID indicated</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a CashBook with OriginId indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("origin/{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> GetOriginId(Guid id)
	{
		_logger.LogInformation("Begin Request for CashBook with OriginId = {id}", id);
		return ServiceResponse(await _applicationCashBookService.GetByOriginId(id));
	}

	/// <summary>
	/// Send a CashBook
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the CashBook inserted
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPost]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Post([FromBody] CashBookDto obj)
	{
		_logger.LogInformation("Begin Request for Create a CashBook({obj})", obj);
		return ServiceResponse(await _applicationCashBookService.Add(obj));
	}

	/// <summary>
	/// Send a Update to CashBook
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the CashBook updated
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Put([FromBody] CashBookUpdateDto obj)
	{
		_logger.LogInformation("Begin Request for Update a CashBook({obj})", obj);
		return ServiceResponse(await _applicationCashBookService.Update(obj));
	}
}
