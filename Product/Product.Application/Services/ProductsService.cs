using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;
using Microsoft.Extensions.Logging;
using Product.Application.Services.Interface;
using Product.Data.Repositories.Interface;
using Product.Domain.Entities;

namespace Product.Application.Services;

public class ProductsService : ServiceBase<Products>, IProductsService
{
	private readonly IProductsRepository _productsRepository;
	private readonly ILogger<ProductsService> _logger;

	public ProductsService(
		ILogger<ProductsService> logger,
		IServiceContext serviceContext,
		IProductsRepository productRepository) : base(logger, productRepository, serviceContext)
	{
		_logger = logger;
		_productsRepository = productRepository;
	}

	public override async Task<Products> AddAsync(Products model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		var resultDescription = await _productsRepository.GetAllAsync(x => x.Description == model.Description);
		if (resultDescription.Count != 0)
			AddNotification("Products can't have same Description");
		var resultGTIN = await _productsRepository.GetAllAsync(x => x.GTIN == model.GTIN);
		if (resultGTIN.Count != 0)
			AddNotification("Products can't have same GTIN");
		if (!IsValidOperation)
			return null;
		return await _productsRepository.AddAsync(model);
	}

	public override async Task<Products> UpdateAsync(Products model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		var resultDescription = await _productsRepository.GetAllAsync(x => x.Description == model.Description);
		if (resultDescription.Count != 0)
			AddNotification("Products can't have same Description");
		var resultGTIN = await _productsRepository.GetAllAsync(x => x.GTIN == model.GTIN);
		if (resultGTIN.Count != 0)
			AddNotification("Products can't have same GTIN");
		if (!IsValidOperation)
			return null;
		return await _productsRepository.UpdateAsync(model);
	}

	public override async Task<PagesBase<Products>> GetAllAsync(int page, int itemsPerPage)
	{
		var result = await _productsRepository.GetAllAsync(page, itemsPerPage);
		if (result.Models.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public async Task<PagesBase<Products>> GetByCategoryAsync(ProductCategory category, int page, int itemsPerPage)
	{
		var result = await _productsRepository.GetAllAsync(x => x.Category == category, page, itemsPerPage);
		if (result.Models.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<Products> GetByIdAsync(Guid id)
	{
		var result = await _productsRepository.GetByIdAsync(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
