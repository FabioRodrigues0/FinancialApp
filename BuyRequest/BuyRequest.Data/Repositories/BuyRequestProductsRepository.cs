using BuyRequest.Data.Repositories.Interfaces;
using BuyRequest.Domain.Models;
using Infrastructure.Shared.Repository;
using Microsoft.Extensions.Logging;

namespace BuyRequest.Data.Repositories;

public class BuyRequestProductsRepository : RepositoryBase<BuyRequestProducts>, IBuyRequestProductsRepository
{
	private readonly HttpClient _http;
	private readonly ILogger<BuyRequestProductsRepository> _logger;

	public BuyRequestProductsRepository(
		ILogger<BuyRequestProductsRepository> logger,
		BuyRequestContext context,
		HttpClient http) : base(logger, context)
	{
		_logger = logger;
		_http = http;
	}
}