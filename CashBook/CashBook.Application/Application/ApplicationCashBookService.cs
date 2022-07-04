using AutoMapper;
using CashBook.Application.Application.Interface;
using CashBook.Application.Models;
using CashBook.Application.Services.Interface;
using CashBook.Domain.Entities;

namespace CashBook.Application.Application;

public class ApplicationCashBookService : IApplicationCashBookService
{
	private readonly ICashBookService _cashBookService;
	private readonly IMapper _mapper;

	public ApplicationCashBookService(
		ICashBookService cashBookService,
		IMapper mapper)
	{
		_cashBookService = cashBookService;
		_mapper = mapper;
	}

	public async Task<CashBooks> AddAsync(CashBookModel obj)
	{
		var result = _mapper.Map<CashBooks>(obj);
		return await _cashBookService.AddAsync(result);
	}

	public async Task<CashBookModel> GetByIdAsync(Guid id)
	{
		var cashBooks = await _cashBookService.GetByIdAsync(id);
		return _mapper.Map<CashBookModel>(cashBooks);
	}

	public async Task<List<CashBookModel>> GetByOriginIdAsync(Guid id)
	{
		var cashBooks = await _cashBookService.GetByOriginIdAsync(id);
		return _mapper.Map<List<CashBookModel>>(cashBooks);
	}

	public async Task<PagesCashBookModel> GetAllAsync(int page, int itemsPerPage)
	{
		var result = await _cashBookService.GetAllAsync(page, itemsPerPage);
		if (result.Models.Count == 0)
			return null;
		return _mapper.Map<PagesCashBookModel>(result);
	}

	public async Task<CashBooks> UpdateAsync(CashBookUpdateModel obj)
	{
		var result = _mapper.Map<CashBooks>(obj);
		return await _cashBookService.UpdateAsync(result);
	}
}
