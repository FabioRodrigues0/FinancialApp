using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Data.Repositories.Interfaces;
using BuyRequest.Domain.Models;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;
using Microsoft.Extensions.Logging;

namespace BuyRequest.Application.Services;

public class BuyRequestProductService : ServiceBase<BuyRequestProducts>, IBuyRequestProductService
{
	private readonly IBuyRequestProductsRepository _buyRequestProductsRepository;
	private readonly ILogger<BuyRequestProductService> _logger;

	public BuyRequestProductService(
		ILogger<BuyRequestProductService> logger,
		IServiceContext serviceContext,
		IBuyRequestProductsRepository buyRequestProductsRepository)
		: base(logger, buyRequestProductsRepository, serviceContext)
	{
		_logger = logger;
		_buyRequestProductsRepository = buyRequestProductsRepository;
	}
}
