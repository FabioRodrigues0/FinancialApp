using AutoMapper;
using BuyRequest.Application.Application.Interface;
using BuyRequest.Application.Models;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Domain.Entities;

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

	public async Task<BuyRequests> AddAsync(BuyRequestModel obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.AddAsync(result);
	}

	public async Task<BuyRequestModel> GetByIdAsync(Guid id)
	{
		var buyRequests = await _buyRequestService.GetByIdAsync(id);
		return _mapper.Map<BuyRequestModel>(buyRequests);
	}

	public async Task<BuyRequestModel> GetByClientIdAsync(Guid id)
	{
		var buyRequests = await _buyRequestService.GetByClientIdAsync(id);
		return _mapper.Map<BuyRequestModel>(buyRequests);
	}

	public async Task<PagesBuyRequestModel> GetAllAsync(int page, int itemsPerPage)
	{
		var result = await _buyRequestService.GetAllAsync(page, itemsPerPage);
		if (result.Models.Count == 0)
			return null;
		return _mapper.Map<PagesBuyRequestModel>(result);
	}

	public async Task<BuyRequests> UpdateAsync(BuyRequestUpdateModel obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.UpdateAsync(result);
	}

	public async Task<BuyRequests> PatchAsync(BuyRequestPatchModel obj)
	{
		var result = _mapper.Map<BuyRequests>(obj);
		return await _buyRequestService.PatchAsync(result);
	}

	public async Task<bool> RemoveAsync(Guid id)
	{
		return await _buyRequestService.RemoveAsync(id);
	}
}
