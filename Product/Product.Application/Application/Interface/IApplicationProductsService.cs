using Infrastructure.Shared.Enums;
using Product.Application.DTO;
using Product.Domain.Models;

namespace Product.Application.Application.Interface;

public interface IApplicationProductsService
{
	Task<Products> AddAsync(ProductsDto obj);

	Task<ProductsDto> GetByIdAsync(Guid id);

	Task<PagesProductsDto> GetByCategoryAsync(ProductCategory category, int page);

	Task<PagesProductsDto> GetAllAsync(int page);

	Task<Products> UpdateAsync(ProductsUpdateDto obj);

	Task<bool> RemoveAsync(Guid id);
}