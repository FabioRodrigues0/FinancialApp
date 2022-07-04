using AutoMapper;
using Infrastructure.Shared.Enums;
using Product.Application.Application.Interface;
using Product.Application.Models;
using Product.Application.Services.Interface;
using Product.Domain.Entities;

namespace Product.Application.Application;

public class ApplicationProductsService : IApplicationProductsService
{
	private readonly IProductsService _productsService;
	private readonly IMapper _mapper;

	public ApplicationProductsService(
		IProductsService productsService,
		IMapper mapper)
	{
		_productsService = productsService;
		_mapper = mapper;
	}

	public async Task<Products> AddAsync(ProductsModel obj)
	{
		var result = _mapper.Map<Products>(obj);
		return await _productsService.AddAsync(result);
	}

	public async Task<ProductsModel> GetByIdAsync(Guid id)
	{
		var result = await _productsService.GetByIdAsync(id);
		return _mapper.Map<ProductsModel>(result);
	}

	public async Task<PagesGetCategoryProductsModel> GetByCategoryAsync(ProductCategory category, int page, int itemsPerPage)
	{
		var result = await _productsService.GetByCategoryAsync(category, page, itemsPerPage);
		if (result.Models.Count == 0)
			return null;
		return _mapper.Map<PagesGetCategoryProductsModel>(result);
	}

	public async Task<PagesGetAllProductsModel> GetAllAsync(int page, int itemsPerPage)
	{
		var result = await _productsService.GetAllAsync(page, itemsPerPage);
		if (result.Models.Count == 0)
			return null;
		return _mapper.Map<PagesGetAllProductsModel>(result);
	}

	public async Task<Products> UpdateAsync(ProductsWithIdModel obj)
	{
		var result = _mapper.Map<Products>(obj);
		return await _productsService.UpdateAsync(result);
	}

	public async Task<bool> RemoveAsync(Guid id)
	{
		return await _productsService.RemoveAsync(id);
	}
}
