using AutoMapper;
using Document.Application.DTO;

namespace Document.Application.Map;

public class PagesDocumentMapper : Profile
{
	public PagesDocumentMapper()
	{
		CreateMap<(List<DocumentDto> list, int totalPages, int page), PagesDocumentDto>()
			.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
			.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
			.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
	}
}
