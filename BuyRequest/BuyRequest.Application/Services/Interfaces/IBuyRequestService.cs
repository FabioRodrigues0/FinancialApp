using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Services.Interface;

namespace BuyRequest.Application.Services.Interfaces;

public interface IBuyRequestService : IServiceBase<BuyRequests>
{
	Task<BuyRequests> GetByClientIdAsync(Guid id);
}
