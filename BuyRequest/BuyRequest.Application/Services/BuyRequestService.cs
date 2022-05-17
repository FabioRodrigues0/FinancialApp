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

	public override async Task<BuyRequests> Add(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.Add(model);
		if (Status.Finished.Equals(model.Status))
		{
			var cashbookDto = _mapper.Map<CashBookDto>(model);
			await _cashBookApiClient.PostAsync(cashbookDto);
		}
		return result;
	}

	public override async Task<BuyRequests> Update(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.GetById(model.Id);
		if (result.Status == Status.Finished && model.Status != Status.Finished)
			AddNotification("Request mark as Finished can't be modified");
		var response = await _buyRequestRepository.Update(model);
		if (response.Status == Status.Finished)
		{
			var cashbookDto = _mapper.Map<CashBookDto>(model);
			await _cashBookApiClient.PostAsync(cashbookDto);
		}
		return response;
	}

	public override async Task<BuyRequests> Patch(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.GetById(model.Id);
		if (result.Status == Status.Finished && model.Status != Status.Finished)
			AddNotification("Request mark as Finished can't be modified");
		var response = await _buyRequestRepository.Patch(model);
		if (model.Status == Status.Finished)
		{
			var cashbookDto = _mapper.Map<CashBookDto>(model);
			await _cashBookApiClient.PostAsync(cashbookDto);
		}

		return response;
	}

	public override async Task<bool> Remove(Guid id)
	{
		_logger.LogInformation("Checks if there are BuyRequests with Id = {id}", id);
		var obj = await _buyRequestRepository.GetById(id);
		if (obj == null)
			return true;
		var result = await _buyRequestRepository.Remove(obj);
		if (obj.Status == Status.Finished)
		{
			var cashbookDto = _mapper.Map<CashBookDto>(result);
			await _cashBookApiClient.PostAsync(cashbookDto);
		}
		return result;
	}

	public async Task<(List<BuyRequests> list, int totalPages, int page)> GetAll(int page)
	{
		var result = await _buyRequestRepository.GetAll(page);
		if (result.list.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public async Task<BuyRequests> GetByClientId(Guid id)
	{
		var result = await _buyRequestRepository.GetByClientIdAsync(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<BuyRequests> GetById(Guid id)
	{
		var result = await _buyRequestRepository.GetById(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
