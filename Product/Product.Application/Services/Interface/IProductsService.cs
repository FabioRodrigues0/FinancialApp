using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Interfaces;
using Product.Domain.Models;

namespace Product.Application.Services.Interface;

public interface IProductsService : IServiceBase<Products>
{
	Task<(List<Products> list, int totalPages, int page)> GetByCategoryAsync(ProductCategory category, int page);
}