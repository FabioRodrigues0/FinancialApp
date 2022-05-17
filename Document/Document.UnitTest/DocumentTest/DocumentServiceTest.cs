using Document.Application.Services;
using Document.Data.Repositories.Interfaces;
using Document.Domain.Models;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace Document.UnitTest.DocumentTest;

public class DocumentServiceTest
{
	public readonly AutoMocker _mocker;

	public DocumentServiceTest()
	{
		_mocker = new AutoMocker();
	}

	[Fact]
	public async Task DocumentService_GetAll()
	{
		//Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;
		int totalPages = 1, page = 1;
		var list = (documentFaker.listModel, totalPages, page); ;

		var repository = _mocker.GetMock<IDocumentRepository>();
		repository.Setup(x => x.GetAllAsync(page)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<DocumentService>();

		//Act
		await service.GetAllAsync(1);

		//Assert
		repository.Verify(x => x.GetAllAsync(1), Times.Once);
	}

	[Fact]
	public async Task DocumentService_GetById()
	{
		//Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var repository = _mocker.GetMock<IDocumentRepository>();
		repository.Setup(x => x.GetByIdAsync(document.Id));

		var service = _mocker.CreateInstance<DocumentService>();

		//Act
		await service.GetByIdAsync(document.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(document.Id), Times.Once);
	}

	[Fact]
	public async Task DocumentService_Add()
	{
		//Arrange

		#region Vars

		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var repository = _mocker.GetMock<IDocumentRepository>();
		repository.Setup(x => x.AddAsync(document));

		var service = _mocker.CreateInstance<DocumentService>();

		#endregion Vars

		//Act
		await service.AddAsync(document);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<Documents>()), Times.Once);
	}

	[Fact]
	public async Task DocumentService_Update()
	{
		//Arrange

		#region Vars

		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var repository = _mocker.GetMock<IDocumentRepository>();
		repository.Setup(x => x.UpdateAsync(document));

		var service = _mocker.CreateInstance<DocumentService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(document);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<Documents>()), Times.Once);
	}

	[Fact]
	public async Task DocumentService_Patch()
	{
		//Arrange

		#region Vars

		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var repository = _mocker.GetMock<IDocumentRepository>();
		var repositoryId = repository.Setup(x => x.GetByIdAsync(document.Id)).ReturnsAsync(document);
		repository.Setup(x => x.PatchAsync(document)).ReturnsAsync(document);

		var service = _mocker.CreateInstance<DocumentService>();

		#endregion Vars

		//Act
		await service.PatchAsync(document);

		//Assert
		repository.Verify(x => x.PatchAsync(It.IsAny<Documents>()), Times.Once);
	}
}