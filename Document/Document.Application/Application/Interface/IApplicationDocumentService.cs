using Document.Application.DTO;
using Document.Domain.Models;

namespace Document.Application.Application.Interface;

public interface IApplicationDocumentService
{
	Task<Documents> AddAsync(DocumentDto obj);

	Task<DocumentDto> GetByIdAsync(Guid id);

	Task<PagesDocumentDto> GetAllAsync(int page);

	Task<Documents> UpdateAsync(DocumentUpdateDto obj);

	Task<Documents> PatchAsync(DocumentPatchDto obj);

	Task<bool> RemoveAsync(Guid id);
}