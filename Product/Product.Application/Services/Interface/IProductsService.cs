using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Services.Interface;
using Product.Domain.Entities;

namespace Product.Application.Services.Interface;

public interface IProductsService : IServiceBase<Products>
{
	Task<PagesBase<Products>> GetByCategoryAsync(ProductCategory category, int page, int itemsPerPage);
}
