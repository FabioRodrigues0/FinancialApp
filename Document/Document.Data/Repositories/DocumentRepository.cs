using Document.Data.Repositories.Interfaces;
using Document.Domain.Models;
using Infrastructure.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Document.Data.Repositories;

public class DocumentRepository : RepositoryBase<Documents>, IDocumentRepository
{
	private readonly ILogger<DocumentRepository> _logger;

	public DocumentRepository(
		ILogger<DocumentRepository> logger,
		DocumentContext context) : base(logger, context)
	{
		_logger = logger;
	}

	public override async Task<bool> RemoveAsync(Guid id)
	{
		var result = await dbSet
				.Where(x => x.Id == id)
				.AsNoTracking()
				.FirstOrDefaultAsync();
		await base.RemoveAsync(id);
		return result != null;
	}

	public virtual async Task<Documents> PatchAsync(Documents obj)
	{
		_logger.LogInformation("Call change to Paid on {obj}", obj);
		var result = await dbSet
			.Where(x => x.Id == obj.Id)
			.AsNoTracking()
			.FirstOrDefaultAsync();
		result.Paid = obj.Paid;
		await base.PatchAsync(result);
		return result;
	}
}
