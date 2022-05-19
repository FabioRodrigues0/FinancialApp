using AutoMapper;
using Product.Application.DTO;

namespace Product.Application.Map;

public class PagesProductsMapper : Profile
{
	public PagesProductsMapper()
	{
		CreateMap<(List<ProductsWithIdDto> list, int totalPages, int page), PagesGetAllProductsDto>()
				.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
				.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
				.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
		CreateMap<(List<ProductsCategoryDto> list, int totalPages, int page), PagesGetCategoryProductsDto>()
				.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
				.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
				.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
	}
}