using AutoMapper;
using Document.Application.DTO;
using Document.Domain.Models;

namespace Document.Application.Map;

public class DocumentAutoMapper : Profile
{
	public DocumentAutoMapper()
	{
		CreateMap<Documents, DocumentDto>().ReverseMap();
		CreateMap<Documents, DocumentUpdateDto>().ReverseMap();
		CreateMap<Documents, DocumentPatchDto>().ReverseMap();
		CreateMap<(List<Documents> list, int totalPages, int page), (List<DocumentDto> list, int totalPages, int page)>().ReverseMap();
	}
}
