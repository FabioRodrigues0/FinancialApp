using BuyRequest.Application.DTO;
using BuyRequest.Domain.Models;

namespace BuyRequest.Application.Application.Interface;

public interface IApplicationBuyRequestService
{
	Task<BuyRequests> AddAsync(BuyRequestDto obj);

	Task<BuyRequestDto> GetByIdAsync(Guid id);

	Task<BuyRequestDto> GetByClientIdAsync(Guid id);

	Task<PagesBuyRequestDto> GetAllAsync(int page);

	Task<BuyRequests> UpdateAsync(BuyRequestUpdateDto obj);

	Task<BuyRequests> PatchAsync(BuyRequestPatchDto obj);

	Task<bool> RemoveAsync(Guid id);
}