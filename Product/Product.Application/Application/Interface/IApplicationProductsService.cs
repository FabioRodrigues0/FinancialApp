using Infrastructure.Shared.Enums;
using Product.Application.Models;
using Product.Domain.Entities;

namespace Product.Application.Application.Interface;

public interface IApplicationProductsService
{
	Task<Products> AddAsync(ProductsModel obj);

	Task<ProductsModel> GetByIdAsync(Guid id);

	Task<PagesGetCategoryProductsModel> GetByCategoryAsync(ProductCategory category, int page, int itemsPerPage);

	Task<PagesGetAllProductsModel> GetAllAsync(int page, int itemsPerPage);

	Task<Products> UpdateAsync(ProductsWithIdModel obj);

	Task<bool> RemoveAsync(Guid id);
}
