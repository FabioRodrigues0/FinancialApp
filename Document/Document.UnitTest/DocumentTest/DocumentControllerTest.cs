using AutoMapper;
using Document.Api.Controllers;
using Document.Application.Application.Interface;
using Document.Application.Map;
using Document.Application.Models;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace Document.UnitTest.DocumentTest;

public class DocumentControllerTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public DocumentControllerTest()
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
	public async Task DocumentController_Post()
	{
		// Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentModel>(document);

		var application = _mocker.GetMock<IApplicationDocumentService>();
		application.Setup(x => x.AddAsync(result));

		var controller = _mocker.CreateInstance<DocumentController>();

		// Act
		await controller.Post(result);

		// Assert
		application.Verify(x => x.AddAsync(It.IsAny<DocumentModel>()), Times.Once);
	}

	[Fact]
	public async Task DocumentController_Get()
	{
		// Arrange
		var application = _mocker.GetMock<IApplicationDocumentService>();
		application.Setup(x => x.GetAllAsync(1, 10));

		var controller = _mocker.CreateInstance<DocumentController>();

		// Act
		await controller.Get(1);

		// Assert
		application.Verify(x => x.GetAllAsync(1, 10), Times.Once);
	}

	[Fact]
	public async Task DocumentController_Update()
	{
		// Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentUpdateModel>(document);

		var application = _mocker.GetMock<IApplicationDocumentService>();
		application.Setup(x => x.UpdateAsync(result));

		var controller = _mocker.CreateInstance<DocumentController>();

		// Act
		await controller.Put(result);

		// Assert
		application.Verify(x => x.UpdateAsync(It.IsAny<DocumentUpdateModel>()), Times.Once);
	}

	[Fact]
	public async Task DocumentController_Patch()
	{
		// Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentPatchModel>(document);

		var application = _mocker.GetMock<IApplicationDocumentService>();
		application.Setup(x => x.PatchAsync(result));

		var controller = _mocker.CreateInstance<DocumentController>();

		// Act
		await controller.Patch(result);

		// Assert
		application.Verify(x => x.PatchAsync(It.IsAny<DocumentPatchModel>()), Times.Once);
	}

	[Fact]
	public async Task DocumentController_Remove()
	{
		// Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentUpdateModel>(document);

		var application = _mocker.GetMock<IApplicationDocumentService>();
		application.Setup(x => x.RemoveAsync(result.Id));

		var controller = _mocker.CreateInstance<DocumentController>();

		// Act
		await controller.Delete(result.Id);

		// Assert
		application.Verify(x => x.RemoveAsync(result.Id), Times.Once);
	}

	[Fact]
	public async Task DocumentController_GetById()
	{
		// Arrange
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentUpdateModel>(document);

		var application = _mocker.GetMock<IApplicationDocumentService>();
		application.Setup(x => x.GetByIdAsync(result.Id));

		var controller = _mocker.CreateInstance<DocumentController>();

		// Act
		await controller.Get(result.Id);

		// Assert
		application.Verify(x => x.GetByIdAsync(result.Id), Times.Once);
	}
}
