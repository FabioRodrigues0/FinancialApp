using Infrastructure.Shared.Controller;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Application.Interface;
using Product.Application.DTO;

namespace Product.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController : ApiControllerBase
{
	private readonly ILogger<ProductsController> _logger;
	private readonly IApplicationProductsService _applicationProductService;

	public ProductsController(
		IApplicationProductsService applicationProductService,
		ILogger<ProductsController> logger,
		IServiceContext context) : base(context)
	{
		_logger = logger;
		_applicationProductService = applicationProductService;
	}

	/// <summary>
	/// Calls 10 Product in Data base
	/// </summary>
	/// <returns>10 Product in Database</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have any Product in Database</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("page/{page}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get(int page)
	{
		_logger.LogInformation("Begin Request for Product {page}", page);
		return ServiceResponse(await _applicationProductService.GetAllAsync(page));
	}

	/// <summary>
	/// Gets Product in Database with ID indicated
	/// </summary>
	/// <returns>Product in Database with ID indicated</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a Product with ID indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Get(Guid id)
	{
		_logger.LogInformation("Begin Request for Product with Id = {id}", id);
		return ServiceResponse(await _applicationProductService.GetByIdAsync(id));
	}

	/// <summary>
	/// Gets Products in Database with Category indicated
	/// </summary>
	/// <returns>Products in Data base with Category indicated</returns>
	/// <response code="200">Success response</response>
	/// <response code="204">Don't have a Products with Category indicated</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpGet("category/{category}/{page}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> GetByCategoryAsync(ProductCategory category, int page= 1)
	{
		_logger.LogInformation("Begin Request for Products with Category = {category}", category);
		return ServiceResponse(await _applicationProductService.GetByCategoryAsync(category, page));
	}

	/// <summary>
	/// Send a Product
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the Product inserted
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPost]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Post([FromBody] ProductsDto obj)
	{
		_logger.LogInformation("Begin Request for Create a Product({obj})", obj);
		return ServiceResponse(await _applicationProductService.AddAsync(obj));
	}

	/// <summary>
	/// Send a Update to Product
	/// </summary>
	/// <returns>
	/// if have errors in validation indicates that if not return errors null and the Product updated
	/// </returns>
	/// <response code="200">Success response</response>
	/// <response code="400">
	/// When a request error occurs but a message reporting the error is returned
	/// </response>
	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Put([FromBody] ProductsUpdateDto obj)
	{
		_logger.LogInformation("Begin Request for Update a Product({obj})", obj);
		return ServiceResponse(await _applicationProductService.UpdateAsync(obj));
	}
}