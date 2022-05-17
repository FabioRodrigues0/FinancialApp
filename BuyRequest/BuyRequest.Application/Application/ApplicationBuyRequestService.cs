using System.Collections.Generic;
using AutoMapper;
using BuyRequest.Application.Application.Interface;
using BuyRequest.Application.DTO;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Domain.Models;
using CashBook.Application.DTO;

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

	public async Task<BuyRequests> Add(BuyRequestDto obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.Add(result);
	}

	public async Task<BuyRequestDto> GetById(Guid id)
	{
		var buyRequests = await _buyRequestService.GetById(id);
		return _mapper.Map<BuyRequestDto>(buyRequests);
	}

	public async Task<BuyRequestDto> GetByClientId(Guid id)
	{
		var buyRequests = await _buyRequestService.GetByClientId(id);
		return _mapper.Map<BuyRequestDto>(buyRequests);
	}

	public async Task<PagesBuyRequestDto> GetAll(int page)
	{
		var result = await _buyRequestService.GetAll(page);
		if (result.list.Count == 0)
			return null;
		var toDto = _mapper.Map<List<BuyRequestDto>>(result.list);
		var newResult = (toDto, result.totalPages, page);
		return _mapper.Map<PagesBuyRequestDto>(newResult);
	}

	public async Task<BuyRequests> Update(BuyRequestUpdateDto obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.Update(result);
	}

	public async Task<BuyRequests> Patch(BuyRequestPatchDto obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.Patch(result);
	}

	public async Task<bool> Remove(Guid id)
	{
		return await _buyRequestService.Remove(id);
	}
}
