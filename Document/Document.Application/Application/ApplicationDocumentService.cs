using AutoMapper;
using CashBook.Application.DTO;
using Document.Application.Application.Interface;
using Document.Application.DTO;
using Document.Application.Services.Interface;
using Document.Domain.Models;

namespace Document.Application.Application;

public class ApplicationDocumentService : IApplicationDocumentService
{
	private readonly IDocumentService _documentService;
	private readonly IMapper _mapper;

	public ApplicationDocumentService(IDocumentService documentService, IMapper mapper)
	{
		_documentService = documentService;
		_mapper = mapper;
	}

	public async Task<Documents> AddAsync(DocumentDto obj)
	{
		var documents = _mapper.Map<Documents>(obj);
		return await _documentService.AddAsync(documents);
	}

	public async Task<DocumentDto> GetByIdAsync(Guid id)
	{
		var documents = await _documentService.GetByIdAsync(id);
		return _mapper.Map<DocumentDto>(documents);
	}

	public async Task<PagesDocumentDto> GetAllAsync(int page)
	{
		var result = await _documentService.GetAllAsync(page);
		if (result.list.Count == 0)
			return null;
		var toDto = _mapper.Map<List<DocumentDto>>(result.list);
		var newResult = (toDto, result.totalPages, page);
		return _mapper.Map<PagesDocumentDto>(newResult);
	}

	public async Task<Documents> UpdateAsync(DocumentUpdateDto obj)
	{
		var result = _mapper.Map<Documents>(obj);
		return await _documentService.UpdateAsync(result);
	}

	public async Task<Documents> PatchAsync(DocumentPatchDto obj)
	{
		var result = _mapper.Map<Documents>(obj);
		return await _documentService.PatchAsync(result);
	}

	public async Task<bool> RemoveAsync(Guid id)
	{
		return await _documentService.RemoveAsync(id);
	}
}
