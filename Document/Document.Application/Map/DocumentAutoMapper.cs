using AutoMapper;
using Document.Application.Models;
using Document.Domain.Models;

namespace Document.Application.Map;

public class DocumentAutoMapper : Profile
{
	public DocumentAutoMapper()
	{
		CreateMap<Documents, DocumentModel>().ReverseMap();
		CreateMap<Documents, DocumentUpdateModel>().ReverseMap();
		CreateMap<Documents, DocumentPatchModel>().ReverseMap();
		CreateMap<(List<Documents> list, int totalPages, int page), (List<DocumentModel> list, int totalPages, int page)>().ReverseMap();
	}
}
