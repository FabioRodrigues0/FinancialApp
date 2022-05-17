using AutoMapper;
using CashBook.Application.DTO;
using CashBook.Application.Services.Interface;
using CashBook.Data.Repositories.Interfaces;
using CashBook.Domain.Models;
using Infrastructure.Shared;
using Infrastructure.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace CashBook.Application.Services;

public class CashBookService : ServiceBase<CashBooks>, ICashBookService
{
	private readonly ICashBookRepository _cashBookRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<CashBookService> _logger;

	public CashBookService(
		ILogger<CashBookService> logger,
		IServiceContext serviceContext,
		ICashBookRepository cashBookRepository,
		IMapper mapper) : base(logger, cashBookRepository, serviceContext)
	{
		_logger = logger;
		_cashBookRepository = cashBookRepository;
		_mapper = mapper;
	}

	public override async Task<CashBooks> Add(CashBooks model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;

		return await _cashBookRepository.Add(model);
	}

	public override async Task<CashBooks> Update(CashBooks model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _cashBookRepository.GetById(model.Id);
		if (result.IsEdited)
			AddNotification("Cash book inserted integration can't be modified");
		return await _cashBookRepository.Update(model);
	}

	public async Task<(List<CashBooks> list, int totalPages, int page)> GetAll(int page)
	{
		var result = await _cashBookRepository.GetAll(page);
		if (result.list.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public async Task<List<CashBooks>> GetByOriginId(Guid id)
	{
		var result = await _cashBookRepository.GetByOriginId(id);
		if (result.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<CashBooks> GetById(Guid id)
	{
		var result = await _cashBookRepository.GetById(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
