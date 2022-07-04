using Document.Application.Models;
using Document.Domain.Models;
using Infrastructure.Shared.Entities;

namespace Document.Application.Application.Interface;

public interface IApplicationDocumentService
{
	Task<Documents> AddAsync(DocumentModel obj);

	Task<DocumentModel> GetByIdAsync(Guid id);

	Task<PagesDocumentModel> GetAllAsync(int page, int itemsPerPage);

	Task<Documents> UpdateAsync(DocumentUpdateModel obj);

	Task<Documents> PatchAsync(DocumentPatchModel obj);

	Task<bool> RemoveAsync(Guid id);
}
