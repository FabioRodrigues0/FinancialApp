using AutoMapper;
using CashBook.Application.Application.Interface;
using CashBook.Application.DTO;
using CashBook.Application.Services.Interface;
using CashBook.Domain.Models;

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

	public async Task<CashBooks> AddAsync(CashBookDto obj)
	{
		var result = _mapper.Map<CashBooks>(obj);
		return await _cashBookService.AddAsync(result);
	}

	public async Task<CashBookDto> GetByIdAsync(Guid id)
	{
		var cashBooks = await _cashBookService.GetByIdAsync(id);
		return _mapper.Map<CashBookDto>(cashBooks);
	}

	public async Task<List<CashBookDto>> GetByOriginIdAsync(Guid id)
	{
		var cashBooks = await _cashBookService.GetByOriginIdAsync(id);
		return _mapper.Map<List<CashBookDto>>(cashBooks);
	}

	public async Task<PagesCashBookDto> GetAllAsync(int page)
	{
		var result = await _cashBookService.GetAllAsync(page);
		if (result.list.Count == 0)
			return null;
		var toDto = _mapper.Map<List<CashBookDto>>(result.list);
		var newResult = (toDto, result.totalPages, page);
		return _mapper.Map<PagesCashBookDto>(newResult);
	}

	public async Task<CashBooks> UpdateAsync(CashBookUpdateDto obj)
	{
		var result = _mapper.Map<CashBooks>(obj);
		return await _cashBookService.UpdateAsync(result);
	}
}
