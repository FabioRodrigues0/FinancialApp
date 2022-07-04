using Infrastructure.Shared.Repository;
using Microsoft.Extensions.Logging;
using Product.Data.Repositories.Interface;
using Product.Domain.Entities;

namespace Product.Data.Repositories;

public class ProductsRepository : RepositoryBase<Products>, IProductsRepository
{
	protected readonly ProductsContext _dataContext;
	private readonly ILogger<ProductsRepository> _logger;

	public ProductsRepository(
		ProductsContext context,
		ILogger<ProductsRepository> logger
		) : base(logger, context)
	{
		_dataContext = context;
		_logger = logger;
	}
}
