using AutoMapper;
using CashBook.ApiClient.Interface;
using CashBook.Application.DTO;
using CashBook.Domain.Models;
using Document.Application.DTO;
using Document.Application.Services.Interface;
using Document.Data.Repositories.Interfaces;
using Document.Domain.Models;
using Infrastructure.Shared;
using Infrastructure.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;

namespace Document.Application.Services;

public class DocumentService : ServiceBase<Documents>, IDocumentService
{
	private readonly IDocumentRepository _documentRepository;
	private readonly ICashBookApiClient _cashBookApiClient;
	private readonly IMapper _mapper;
	private readonly ILogger<DocumentService> _logger;

	public DocumentService(
		IMapper mapper,
		ILogger<DocumentService> logger,
		IServiceContext serviceContext,
		IDocumentRepository documentRepository)
		: base(logger, documentRepository, serviceContext)
	{
		_logger = logger;
		_documentRepository = documentRepository;
		_mapper = mapper;
	}

	public override async Task<Documents> Add(Documents model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;

		var obj = await _documentRepository.Add(model);
		if (model.Paid)
		{
			var cashbook = _mapper.Map<CashBookDto>(obj);
		}
		return obj;
	}

	public override async Task<Documents> Update(Documents model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;

		var obj = await _documentRepository.Update(model);
		if (model.Paid)
		{
			var cashBook = _mapper.Map<CashBookDto>(obj);
		}
		return obj;
	}

	public override async Task<Documents> Patch(Documents model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		var result = await _documentRepository.GetById(model.Id);
		if (!IsValidOperation)
			return null;
		var obj = await _documentRepository.Patch(model);
		if (model.Paid)
		{
			var cashBook = _mapper.Map<CashBookDto>(obj);
			await _cashBookApiClient.PostAsync(cashBook);
		}
		return obj;
	}

	public override async Task<bool> Remove(Guid id)
	{
		_logger.LogInformation("Checks if there are Docuemnt with Id = {id}", id);
		var obj = await _documentRepository.GetById(id);
		if (obj == null)
			return true;
		var result = await _documentRepository.Remove(id);
		if (obj.Paid)
		{
			var cashBook = _mapper.Map<CashBookDto>(obj);
			await _cashBookApiClient.PostAsync(cashBook);
		};
		return result;
	}

	public async Task<(List<Documents> list, int totalPages, int page)> GetAll(int page)
	{
		var result = await _documentRepository.GetAll(page);
		if (result.list.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<Documents> GetById(Guid id)
	{
		var result = await _documentRepository.GetById(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
