using AutoMapper;
using Document.Application.Application;
using Document.Application.DTO;
using Document.Application.Map;
using Document.Application.Services.Interface;
using Document.Domain.Models;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace Document.UnitTest.DocumentTest;

public class DocumentApplicationServiceTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public DocumentApplicationServiceTest()
	{
		_mocker = new AutoMocker();
		if (_mapper == null)
		{
			var mapConfig = new MapperConfiguration(x =>
			{
				x.AddProfile(new DocumentAutoMapper());
			});
			_mapper = mapConfig.CreateMapper();
		}
	}

	[Fact]
	public async Task DocumentApplicationService_GetAll()
	{
		//Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;
		int totalPages = 1, page = 1;
		var list = (documentFaker.listModel, totalPages, page); ;

		var repository = _mocker.GetMock<IDocumentService>();
		repository.Setup(x => x.GetAllAsync(page)).ReturnsAsync(list);

		var service = _mocker.CreateInstance<ApplicationDocumentService>();

		//Act
		await service.GetAllAsync(page);

		//Assert
		repository.Verify(x => x.GetAllAsync(page), Times.Once);
	}

	[Fact]
	public async Task DocumentApplicationService_GetById()
	{
		//Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var repository = _mocker.GetMock<IDocumentService>();
		repository.Setup(x => x.GetByIdAsync(document.Id));

		var service = _mocker.CreateInstance<ApplicationDocumentService>();

		//Act
		await service.GetByIdAsync(document.Id);

		//Assert
		repository.Verify(x => x.GetByIdAsync(document.Id), Times.Once);
	}

	[Fact]
	public async Task DocumentApplicationService_Add()
	{
		//Arrange

		#region Vars

		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentDto>(document);

		var repository = _mocker.GetMock<IDocumentService>();
		repository.Setup(x => x.AddAsync(document));

		var service = _mocker.CreateInstance<ApplicationDocumentService>();

		#endregion Vars

		//Act
		await service.AddAsync(result);

		//Assert
		repository.Verify(x => x.AddAsync(It.IsAny<Documents>()), Times.Once);
	}

	[Fact]
	public async Task DocumentApplicationService_Update()
	{
		//Arrange

		#region Vars

		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentUpdateDto>(document);

		var repository = _mocker.GetMock<IDocumentService>();
		repository.Setup(x => x.UpdateAsync(document));

		var service = _mocker.CreateInstance<ApplicationDocumentService>();

		#endregion Vars

		//Act
		await service.UpdateAsync(result);

		//Assert
		repository.Verify(x => x.UpdateAsync(It.IsAny<Documents>()), Times.Once);
	}

	[Fact]
	public async Task DocumentApplicationService_Patch()
	{
		//Arrange

		#region Vars

		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentPatchDto>(document);

		var repository = _mocker.GetMock<IDocumentService>();
		repository.Setup(x => x.PatchAsync(document));

		var service = _mocker.CreateInstance<ApplicationDocumentService>();

		#endregion Vars

		//Act
		await service.PatchAsync(result);

		//Assert
		repository.Verify(x => x.PatchAsync(It.IsAny<Documents>()), Times.Once);
	}
}
