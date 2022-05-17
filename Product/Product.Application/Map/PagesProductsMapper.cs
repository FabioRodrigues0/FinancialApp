using AutoMapper;
using Product.Application.DTO;

namespace Product.Application.Map;

public class PagesProductsMapper : Profile
{
	public PagesProductsMapper()
	{
		CreateMap<(List<ProductsDto> list, int totalPages, int page), PagesProductsDto>()
				.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
				.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
				.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
		CreateMap<(List<ProductsCategoryDto> list, int totalPages, int page), PagesProductsDto>()
				.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
				.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
				.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
	}
}