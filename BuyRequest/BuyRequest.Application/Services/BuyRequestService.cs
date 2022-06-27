using AutoMapper;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Data.Repositories.Interfaces;
using BuyRequest.Domain.Models;
using CashBook.Application.DTO;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;
using MessageBroker.Publisher.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stock.Application.DTO;

namespace BuyRequest.Application.Services;

public class BuyRequestService : ServiceBase<BuyRequests>, IBuyRequestService
{
	private readonly IBuyRequestRepository _buyRequestRepository;
	private readonly ICashBookPublisher _cashBookPublisher;
	private readonly IMovementsPublisher _movementsPublisher;
	private readonly IMapper _mapper;
	private readonly RabbitMqOptions _options;
	private readonly ILogger<BuyRequestService> _logger;
	private readonly decimal d = 0;
	private readonly string queueNameMP;
	private readonly string queueName;

	public BuyRequestService(
		IOptions<RabbitMqOptions> options,
		ILogger<BuyRequestService> logger,
		IServiceContext serviceContext,
		IMapper mapper,
		ICashBookPublisher cashBookPublisher,
		IMovementsPublisher movementsPublisher,
		IBuyRequestRepository buyRequestRepository) : base(logger, buyRequestRepository, serviceContext)
	{
		_options = options.Value;
		_logger = logger;
		_mapper = mapper;
		_cashBookPublisher = cashBookPublisher;
		_movementsPublisher = movementsPublisher;
		_buyRequestRepository = buyRequestRepository;

		queueName = _options.GetQueueName();
		queueNameMP = _options.GetQueueNameMP();
	}

	public override async Task<BuyRequests> AddAsync(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.AddAsync(model);
		if (Status.Finished.Equals(model.Status))
		{
			var cashbookDto = _mapper.Map<CashBookDto>((model, TypeRequest.Add, d));
			_cashBookPublisher.SendToRabbit(cashbookDto, queueName);
		}
		var movementsDto = _mapper.Map<MovementsDto>(model);
		_movementsPublisher.SendToRabbit(movementsDto, queueNameMP);
		return result;
	}

	public override async Task<BuyRequests> UpdateAsync(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.GetByIdAsync(model.Id);
		if (result.Status == Status.Finished && model.Status != Status.Finished)
			AddNotification("Request mark as Finished can't be modified");
		var response = await _buyRequestRepository.UpdateAsync(model);
		if (response.Status == Status.Finished)
		{
			var dif = model.TotalValor - response.TotalValor;
			var cashbookDto = _mapper.Map<CashBookDto>((model, TypeRequest.Update, dif));
			_cashBookPublisher.SendToRabbit(cashbookDto, queueName);
		}
		var movementsDto = _mapper.Map<MovementsDto>(model);
		_movementsPublisher.SendToRabbit(movementsDto, queueNameMP);
		return response;
	}

	public override async Task<BuyRequests> PatchAsync(BuyRequests model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _buyRequestRepository.GetByIdAsync(model.Id);
		if (result.Status == Status.Finished && model.Status != Status.Finished)
			AddNotification("Request mark as Finished can't be modified");
		var response = await _buyRequestRepository.PatchAsync(model);
		if (model.Status == Status.Finished)
		{
			var dif = model.TotalValor - response.TotalValor;
			var cashbookDto = _mapper.Map<CashBookDto>((model, TypeRequest.Patch, dif));
			_cashBookPublisher.SendToRabbit(cashbookDto, queueName);
		}
		var movementsDto = _mapper.Map<MovementsDto>(model);
		_movementsPublisher.SendToRabbit(movementsDto, queueNameMP);
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
			var cashbookDto = _mapper.Map<CashBookDto>((obj, TypeRequest.Remove, d));
			_cashBookPublisher.SendToRabbit(cashbookDto, queueName);
		}
		var movementsDto = _mapper.Map<MovementsDto>(obj);
		_movementsPublisher.SendToRabbit(movementsDto, queueNameMP);
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
