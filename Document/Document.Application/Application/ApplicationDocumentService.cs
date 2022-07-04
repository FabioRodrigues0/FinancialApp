using AutoMapper;
using Document.Application.Application.Interface;
using Document.Application.Models;
using Document.Application.Services.Interface;
using Document.Domain.Models;
using Infrastructure.Shared.Entities;

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

	public async Task<Documents> AddAsync(DocumentModel obj)
	{
		var documents = _mapper.Map<Documents>(obj);
		return await _documentService.AddAsync(documents);
	}

	public async Task<DocumentModel> GetByIdAsync(Guid id)
	{
		var documents = await _documentService.GetByIdAsync(id);
		return _mapper.Map<DocumentModel>(documents);
	}

	public async Task<PagesDocumentModel> GetAllAsync(int page, int itemsPerPage)
	{
		var result = await _documentService.GetAllAsync(page, itemsPerPage);
		return _mapper.Map<PagesDocumentModel>(result);
	}

	public async Task<Documents> UpdateAsync(DocumentUpdateModel obj)
	{
		var result = _mapper.Map<Documents>(obj);
		return await _documentService.UpdateAsync(result);
	}

	public async Task<Documents> PatchAsync(DocumentPatchModel obj)
	{
		var result = _mapper.Map<Documents>(obj);
		return await _documentService.PatchAsync(result);
	}

	public async Task<bool> RemoveAsync(Guid id)
	{
		return await _documentService.RemoveAsync(id);
	}
}
