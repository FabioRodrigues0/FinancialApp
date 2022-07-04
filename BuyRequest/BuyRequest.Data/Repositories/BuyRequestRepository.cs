using BuyRequest.Data.Repositories.Interfaces;
using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuyRequest.Data.Repositories;

public class BuyRequestRepository : RepositoryBase<BuyRequests>, IBuyRequestRepository
{
	private readonly IBuyRequestProductsRepository _productsRepository;
	private readonly ILogger<BuyRequestRepository> _logger;

	public BuyRequestRepository(
		ILogger<BuyRequestRepository> logger,
		BuyRequestContext context,
		IBuyRequestProductsRepository productsRepository) : base(logger, context)
	{
		_logger = logger;
		_productsRepository = productsRepository;
		SetInclude(x => x.Include(z => z.Products));
	}

	public override async Task<BuyRequests> AddAsync(BuyRequests obj)
	{
		_logger.LogInformation("Create new BuyRequest");
		await dbSet.AddAsync(obj);
		await _dataContext.SaveChangesAsync();
		return obj;
	}

	public override async Task<BuyRequests> GetByIdAsync(Guid id)
	{
		_logger.LogInformation("Call a BuyRequest with Id = {id}", id);
		var query = dbSet.Where(br => br.Id == id);
		if (Include != null)
			query = Include(query);
		return await query.AsNoTracking().FirstOrDefaultAsync();
	}

	public async Task<BuyRequests> GetByClientIdAsync(Guid id)
	{
		_logger.LogInformation("Call a BuyRequest with ClientId = {id}", id);
		var query = dbSet.Where(br => br.Client == id);
		if (Include != null)
			query = Include(query);
		return await query.AsNoTracking().FirstOrDefaultAsync();
	}

	public override async Task<BuyRequests> PatchAsync(BuyRequests obj)
	{
		_logger.LogInformation("Call change to Status on {obj}", obj);
		var result = await dbSet
			.Where(x => x.Id == obj.Id)
			.AsNoTracking()
			.FirstOrDefaultAsync();
		result.Status = obj.Status;
		return await base.PatchAsync(result);
	}

	public override async Task<bool> RemoveAsync(Guid id)
	{
		var result = await dbSet
			.Where(x => x.Id == id)
			.AsNoTracking()
			.FirstOrDefaultAsync();
		await base.RemoveAsync(result);
		return result != null;
	}

	public override async Task<BuyRequests> UpdateAsync(BuyRequests obj)
	{
		var result = await dbSet
			.Where(x => x.Id == obj.Id)
			.Include(i => i.Products)
			.AsNoTracking()
			.FirstOrDefaultAsync();

		var productsIds = obj.Products.Select(s => s.Id).ToList();
		var productsToDelete = result.Products.Where(w => !productsIds.Contains(w.Id)).ToList();
		foreach (var produto in productsToDelete)
		{
			await _productsRepository.RemoveAsync(produto.Id);
		}
		return await base.UpdateAsync(obj);
	}
}
