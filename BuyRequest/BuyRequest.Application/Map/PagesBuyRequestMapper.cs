using AutoMapper;
using BuyRequest.Application.DTO;

namespace BuyRequest.Application.Map;

public class PagesBuyRequestMapper : Profile
{
	public PagesBuyRequestMapper()
	{
		CreateMap<(List<BuyRequestDto> list, int totalPages, int page), PagesBuyRequestDto>()
			.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
			.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
			.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
	}
}
