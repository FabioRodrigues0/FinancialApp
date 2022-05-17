using BuyRequest.Application.DTO;
using BuyRequest.Domain.Models;
using Infrastructure.Shared.Interfaces;

namespace BuyRequest.Application.Services.Interfaces;

public interface IBuyRequestService : IServiceBase<BuyRequests>
{
	Task<BuyRequests> GetByClientIdAsync(Guid id);
}