using System.Globalization;
using Infrastructure.Shared;
using Infrastructure.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Data;
using Product.Data.Repositories.Interfaces;
using Product.Domain.Models;

namespace Product.Data.Repositories;

public class ProductsRepository : RepositoryBase<Products>, IProductsRepository
{
	private readonly ILogger<ProductsRepository> _logger;

	public ProductsRepository(
		ProductsContext context,
		ILogger<ProductsRepository> logger
		) : base(logger, context)
	{
		_logger = logger;
	}

	public async Task<bool> ExistAsyncSameDescription(string description)
	{
		_logger.LogInformation("Checks if have any Products with description({description})", description);
		return await dbSet.Where(w => w.Description == description).AnyAsync();
	}

	public async Task<bool> ExistAsyncSameGTIN(string GTIN)
	{
		_logger.LogInformation("Checks if have any Products with GTIN({GTIN})", GTIN);
		return (await dbSet.Where(w => string.Compare(w.GTIN, GTIN) == 0).ToListAsync()).Count() > 0;
	}

	async Task<(List<Products> list, int totalPages, int page)> IProductsRepository.GetByCategoryAsync(ProductCategory category, int page)
	{
		const int pageResults = 10;

		_logger.LogInformation("Calls Products with Category {category}", category);
		var query = dbSet
			.Where(x => x.Category == category)
			.AsNoTracking()
			.Skip((page - 1) * dbSet.Count())
			.Take(pageResults);

		if (Include != null)
			query = Include(query);
		_logger.LogInformation("Calls a List {T}", query.GetType());
		var result = await query.ToListAsync();
		var totalPages = (int)Math.Ceiling(query.Count() / 10f);
		return (result, totalPages, page);
	}
}