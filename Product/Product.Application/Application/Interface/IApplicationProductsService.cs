using Infrastructure.Shared.Enums;
using Product.Application.DTO;
using Product.Domain.Models;

namespace Product.Application.Application.Interface;

public interface IApplicationProductsService
{
	Task<Products> AddAsync(ProductsDto obj);

	Task<ProductsDto> GetByIdAsync(Guid id);

	Task<PagesGetCategoryProductsDto> GetByCategoryAsync(ProductCategory category, int page);

	Task<PagesGetAllProductsDto> GetAllAsync(int page);

	Task<Products> UpdateAsync(ProductsWithIdDto obj);

	Task<bool> RemoveAsync(Guid id);
}