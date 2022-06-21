using AutoMapper;
using BuyRequest.Application.Application.Interface;
using BuyRequest.Application.DTO;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Domain.Models;

namespace BuyRequest.Application.Application;

public class ApplicationBuyRequestService : IApplicationBuyRequestService
{
	private readonly IBuyRequestService _buyRequestService;
	private readonly IMapper _mapper;

	public ApplicationBuyRequestService(
		IBuyRequestService buyRequestService,
		IMapper mapper)
	{
		_buyRequestService = buyRequestService;
		_mapper = mapper;
	}

	public async Task<BuyRequests> AddAsync(BuyRequestDto obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.AddAsync(result);
	}

	public async Task<BuyRequestDto> GetByIdAsync(Guid id)
	{
		var buyRequests = await _buyRequestService.GetByIdAsync(id);
		return _mapper.Map<BuyRequestDto>(buyRequests);
	}

	public async Task<BuyRequestDto> GetByClientIdAsync(Guid id)
	{
		var buyRequests = await _buyRequestService.GetByClientIdAsync(id);
		return _mapper.Map<BuyRequestDto>(buyRequests);
	}

	public async Task<PagesBuyRequestDto> GetAllAsync(int page)
	{
		var result = await _buyRequestService.GetAllAsync(page);
		if (result.list.Count == 0)
			return null;
		var toDto = _mapper.Map<List<BuyRequestDto>>(result.list);
		var newResult = (toDto, result.totalPages, page);
		return _mapper.Map<PagesBuyRequestDto>(newResult);
	}

	public async Task<BuyRequests> UpdateAsync(BuyRequestUpdateDto obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.UpdateAsync(result);
	}

	public async Task<BuyRequests> PatchAsync(BuyRequestPatchDto obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.PatchAsync(result);
	}

	public async Task<bool> RemoveAsync(Guid id)
	{
		return await _buyRequestService.RemoveAsync(id);
	}
}
