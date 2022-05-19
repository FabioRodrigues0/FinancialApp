using AutoMapper;
using Infrastructure.Shared.Enums;
using Product.Application.Application.Interface;
using Product.Application.DTO;
using Product.Application.Services.Interface;
using Product.Domain.Models;

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

	public async Task<Products> AddAsync(ProductsDto obj)
	{
		var result = _mapper.Map<Products>(obj);
		return await _productsService.AddAsync(result);
	}

	public async Task<ProductsDto> GetByIdAsync(Guid id)
	{
		var result = await _productsService.GetByIdAsync(id);
		return _mapper.Map<ProductsDto>(result);
	}

	public async Task<PagesGetCategoryProductsDto> GetByCategoryAsync(ProductCategory category, int page)
	{
		var result = await _productsService.GetByCategoryAsync(category, page);
		if (result.list.Count == 0)
			return null;
		var toDto = _mapper.Map<List<ProductsCategoryDto>>(result.list);
		var newResult = (toDto, result.totalPages, result.page);
		return _mapper.Map<PagesGetCategoryProductsDto>(newResult);
	}

	public async Task<PagesGetAllProductsDto> GetAllAsync(int page)
	{
		var result = await _productsService.GetAllAsync(page);
		if (result.list.Count == 0)
			return null;
		var toDto = _mapper.Map<List<ProductsWithIdDto>>(result.list);
		var newResult = (toDto, result.totalPages, page);
		return _mapper.Map<PagesGetAllProductsDto>(newResult);
	}

	public async Task<Products> UpdateAsync(ProductsWithIdDto obj)
	{
		var result = _mapper.Map<Products>(obj);
		return await _productsService.UpdateAsync(result);
	}

	public async Task<bool> RemoveAsync(Guid id)
	{
		return await _productsService.RemoveAsync(id);
	}
}