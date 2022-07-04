using AutoMapper;
using Document.Application.Models;

namespace Document.Application.Map;

public class PagesDocumentMapper : Profile
{
	public PagesDocumentMapper()
	{
		CreateMap<(List<DocumentModel> list, int totalPages, int page), PagesDocumentModel>()
			.ForMember(to => to.Models, from => from.MapFrom(x => x.list))
			.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.page))
			.ForMember(to => to.Pages, from => from.MapFrom(x => x.totalPages));
	}
}
