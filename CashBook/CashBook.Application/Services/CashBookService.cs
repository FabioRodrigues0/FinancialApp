using AutoMapper;
using CashBook.Application.Services.Interface;
using CashBook.Data.Repositories.Interfaces;
using CashBook.Domain.Models;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;
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

	public override async Task<CashBooks> AddAsync(CashBooks model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		//AddNotification("Erro de negocio");
		if (!IsValidOperation)
			return null;

		return await _cashBookRepository.AddAsync(model);
	}

	public override async Task<CashBooks> UpdateAsync(CashBooks model)
	{
		_logger.LogInformation("Begin Validate {model}", model);
		ValidateEntity(model);
		if (!IsValidOperation)
			return null;
		var result = await _cashBookRepository.GetByIdAsync(model.Id);
		if (result.IsEdited)
			AddNotification("Cash book inserted integration can't be modified");
		return await _cashBookRepository.UpdateAsync(model);
	}

	public async Task<(List<CashBooks> list, int totalPages, int page)> GetAllAsync(int page)
	{
		var result = await _cashBookRepository.GetAllAsync(page);
		if (result.list.Count() == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public async Task<List<CashBooks>> GetByOriginIdAsync(Guid id)
	{
		var result = await _cashBookRepository.GetByOriginId(id);
		if (result.Count == 0)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}

	public override async Task<CashBooks> GetByIdAsync(Guid id)
	{
		var result = await _cashBookRepository.GetByIdAsync(id);
		if (result == null)
		{
			_logger.LogInformation("No Content");
			NoContent(false);
		}
		return result;
	}
}
