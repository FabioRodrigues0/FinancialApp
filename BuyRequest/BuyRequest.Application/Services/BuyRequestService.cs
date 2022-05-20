using System.Text;
using AutoMapper;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Data.Repositories.Interfaces;
using BuyRequest.Domain.Models;
using CashBook.ApiClient.Interface;
using CashBook.Application.DTO;
using Infrastructure.Shared;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BuyRequest.Application.Services;

public class BuyRequestService : ServiceBase<BuyRequests>, IBuyRequestService
{
	private readonly IBuyRequestRepository _buyRequestRepository;
	private readonly IBuyRequestProductService _buyRequestProductService;
	private readonly ICashBookApiClient _cashBookApiClient;
	private readonly IMapper _mapper;
	private readonly ILogger<BuyRequestService> _logger;

	public BuyRequestService(
		ILogger<BuyRequestService> logger,
		IServiceContext serviceContext,
		IMapper mapper,
		ICashBookApiClient cashBookApiClient,
		IBuyRequestRepository buyRequestRepository,
		IBuyRequestProductService buyRequestProductService) : base(logger, buyRequestRepository, serviceContext)
	{
		_logger = logger;
		_mapper = mapper;
		_cashBookApiClient = cashBookApiClient;
		_buyRequestRepository = buyRequestRepository;
		_buyRequestProductService = buyRequestProductService;
	}

	public override async Task<BuyRequests> AddAsync(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.AddAsync(model);
		if (Status.Finished.Equals(model.Status))
		{
			var cashbookDto = _mapper.Map<CashBookDto>(model);
			_cashBookApiClient.SendToRabbit(cashbookDto);
		}
		return result;
	}

	public override async Task<BuyRequests> UpdateAsync(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.GetByIdAsync(model.Id);
		if (result.Status == Status.Finished && model.Status != Status.Finished)
			AddNotification("Request mark as Finished can't be modified");
		var response = await _buyRequestRepository.UpdateAsync(model);
		if (response.Status == Status.Finished)
		{
			var cashbookDto = _mapper.Map<CashBookDto>(model);
			_cashBookApiClient.SendToRabbit(cashbookDto);
		}
		return response;
	}

	public override async Task<BuyRequests> PatchAsync(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.GetByIdAsync(model.Id);
		if (result.Status == Status.Finished && model.Status != Status.Finished)
			AddNotification("Request mark as Finished can't be modified");
		var response = await _buyRequestRepository.PatchAsync(model);
		if (model.Status == Status.Finished)
		{
			var cashbookDto = _mapper.Map<CashBookDto>(model);
			_cashBookApiClient.SendToRabbit(cashbookDto);
		}

		return response;
	}

	public override async Task<bool> RemoveAsync(Guid id)
	{
		_logger.LogInformation("Checks if there are BuyRequests with Id = {id}", id);
		var obj = await _buyRequestRepository.GetByIdAsync(id);
		if (obj == null)
			return true;
		var result = await _buyRequestRepository.RemoveAsync(obj);
		if (obj.Status == Status.Finished)
		{
			var cashbookDto = _mapper.Map<CashBookDto>(result);
			_cashBookApiClient.SendToRabbit(cashbookDto);
		}
		return result;
	}

	public override async Task<(List<BuyRequests> list, int totalPages, int page)> GetAllAsync(int page)
	{
		var result = await _buyRequestRepository.GetAllAsync(page);
		if (result.list == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public async Task<BuyRequests> GetByClientIdAsync(Guid id)
	{
		var result = await _buyRequestRepository.GetByClientIdAsync(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<BuyRequests> GetByIdAsync(Guid id)
	{
		var result = await _buyRequestRepository.GetByIdAsync(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
