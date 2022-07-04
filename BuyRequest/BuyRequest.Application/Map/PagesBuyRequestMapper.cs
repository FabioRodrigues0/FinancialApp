using AutoMapper;
using BuyRequest.Application.Models;
using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Entities;

namespace BuyRequest.Application.Map;

public class PagesBuyRequestMapper : Profile
{
	public PagesBuyRequestMapper()
	{
		CreateMap<PagesBase<BuyRequests>, PagesBuyRequestModel>()
			.ForMember(to => to.Models, from => from.MapFrom(x => x.Models))
			.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.CurrentPage))
			.ForMember(to => to.Pages, from => from.MapFrom(x => x.Pages));
	}
}
