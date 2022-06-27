using BuyRequest.Domain.Models;
using Infrastructure.Shared.Repository.Interface;

namespace BuyRequest.Data.Repositories.Interfaces;

public interface IBuyRequestRepository : IRepositoryBase<BuyRequests>
{
	Task<BuyRequests> GetByClientIdAsync(Guid id);
}