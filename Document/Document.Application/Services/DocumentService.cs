using AutoMapper;
using CashBook.Application.Models;
using Document.Application.Services.Interface;
using Document.Data.Repositories.Interfaces;
using Document.Domain.Models;
using Infrastructure.Shared.Entities;
using Infrastructure.Shared.Enums;
using Infrastructure.Shared.Messaging.Settings;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;
using MessageBroker.Publisher.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Document.Application.Services;

public class DocumentService : ServiceBase<Documents>, IDocumentService
{
	private readonly IDocumentRepository _documentRepository;
	private readonly ICashBookPublisher _cashBookPublisher;
	private readonly RabbitMqOptions _options;
	private readonly IMapper _mapper;
	private readonly ILogger<DocumentService> _logger;
	private readonly decimal d = 0;
	private readonly string queueName;

	public DocumentService(
		IOptions<RabbitMqOptions> options,
		IMapper mapper,
		ICashBookPublisher cashBookPublisher,
		ILogger<DocumentService> logger,
		IServiceContext serviceContext,
		IDocumentRepository documentRepository)
		: base(logger, documentRepository, serviceContext)
	{
		_cashBookPublisher = cashBookPublisher;
		_logger = logger;
		_documentRepository = documentRepository;
		_options = options.Value;
		_mapper = mapper;

		queueName = _options.GetQueueName();
	}

	public override async Task<Documents> AddAsync(Documents model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;

		var obj = await _documentRepository.AddAsync(model);
		if (model.Paid)
		{
			var cashbookDto = _mapper.Map<CashBookModel>((model, TypeRequest.Add, d));
			_cashBookPublisher.SendToRabbit(cashbookDto, queueName);
		}
		return obj;
	}

	public override async Task<Documents> UpdateAsync(Documents model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		await ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;

		var obj = await _documentRepository.UpdateAsync(model);
		if (model.Paid)
		{
			var dif = model.Total - obj.Total;
			var cashbookDto = _mapper.Map<CashBookModel>((obj, TypeRequest.Update, dif));
			_cashBookPublisher.SendToRabbit(cashbookDto, queueName);
		}
		return obj;
	}

	public override async Task<Documents> PatchAsync(Documents model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		var result = await _documentRepository.GetByIdAsync(model.Id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
			return result;
		}
		result.Paid = model.Paid;
		var obj = await _documentRepository.PatchAsync(result);
		if (model.Paid)
		{
			var dif = model.Total - obj.Total;
			var cashBook = _mapper.Map<CashBookModel>((obj, TypeRequest.Patch, dif));
			_cashBookPublisher.SendToRabbit(cashBook, queueName);
		}
		return obj;
	}

	public override async Task<bool> RemoveAsync(Guid id)
	{
		_logger.LogInformation("Checks if there are Docuemnt with Id = {id}", id);
		var obj = await _documentRepository.GetByIdAsync(id);
		if (obj == null)
			return true;
		var result = await _documentRepository.RemoveAsync(id);
		if (obj.Paid)
		{
			var cashBook = _mapper.Map<CashBookModel>((obj, TypeRequest.Remove, d));
			_cashBookPublisher.SendToRabbit(cashBook, queueName);
		};
		return result;
	}

	public override async Task<PagesBase<Documents>> GetAllAsync(int page, int itemsPerPage)
	{
		var result = await _documentRepository.GetAllAsync(page, itemsPerPage);
		if (result.Models.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<Documents> GetByIdAsync(Guid id)
	{
		var result = await _documentRepository.GetByIdAsync(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
