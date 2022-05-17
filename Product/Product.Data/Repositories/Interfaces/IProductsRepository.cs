using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Interfaces;
using Product.Domain.Models;

namespace Product.Data.Repositories.Interfaces;

public interface IProductsRepository : IRepositoryBase<Products>
{
	Task<(List<Products> list, int totalPages, int page)> GetByCategoryAsync(ProductCategory category, int page);
	Task<bool> ExistAsyncSameDescription(string description);
	Task<bool> ExistAsyncSameGTIN(string GTIN);
}