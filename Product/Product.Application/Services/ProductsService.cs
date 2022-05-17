using AutoMapper;
using Infrastructure.Shared;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Product.Application.Services.Interface;
using Product.Data.Repositories.Interfaces;
using Product.Domain.Models;

namespace Product.Application.Services;

public class ProductsService : ServiceBase<Products>, IProductsService
{
	private readonly IProductsRepository _productsRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<ProductsService> _logger;

	public ProductsService(
		ILogger<ProductsService> logger,
		IServiceContext serviceContext,
		IProductsRepository productRepository,
		IMapper mapper) : base(logger, productRepository, serviceContext)
	{
		_logger = logger;
		_productsRepository = productRepository;
		_mapper = mapper;
	}

	public override async Task<Products> AddAsync(Products model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		var resultDescription = await _productsRepository.ExistAsyncSameDescription(model.Description);
		if (resultDescription)
			AddNotification("Products can't have same Description");
		var resultGTIN = await _productsRepository.ExistAsyncSameGTIN(model.GTIN);
		if (resultGTIN)
			AddNotification("Products can't have same GTIN");
		if (!IsValidOperation)
			return null;
		return await _productsRepository.AddAsync(model);
	}

	public override async Task<Products> UpdateAsync(Products model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		var resultDescription = await _productsRepository.ExistAsyncSameDescription(model.Description);
		if (resultDescription)
			AddNotification("Products can't have same Description");
		var resultGTIN = await _productsRepository.ExistAsyncSameGTIN(model.GTIN);
		if (resultGTIN)
			AddNotification("Products can't have same GTIN");
		if (!IsValidOperation)
			return null;
		return await _productsRepository.UpdateAsync(model);
	}

	public override async Task<(List<Products> list, int totalPages, int page)> GetAllAsync(int page)
	{
		var result = await _productsRepository.GetAllAsync(page);
		if (result.list.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public async Task<(List<Products> list, int totalPages, int page)> GetByCategoryAsync(ProductCategory category, int page)
	{
		var result = await _productsRepository.GetByCategoryAsync(category, page);
		if (result.list.Count == 0)
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