using BuyRequest.Application.Models;
using BuyRequest.Domain.Entities;

namespace BuyRequest.Application.Application.Interface;

public interface IApplicationBuyRequestService
{
	Task<BuyRequests> AddAsync(BuyRequestModel obj);

	Task<BuyRequestModel> GetByIdAsync(Guid id);

	Task<BuyRequestModel> GetByClientIdAsync(Guid id);

	Task<PagesBuyRequestModel> GetAllAsync(int page, int itemsPerPage);

	Task<BuyRequests> UpdateAsync(BuyRequestUpdateModel obj);

	Task<BuyRequests> PatchAsync(BuyRequestPatchModel obj);

	Task<bool> RemoveAsync(Guid id);
}
