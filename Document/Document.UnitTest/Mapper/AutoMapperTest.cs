using AutoMapper;
using Document.Application.Map;
using Document.Application.Models;
using Document.UnitTest.DocumentTest;
using Moq.AutoMock;
using Shouldly;
using Xunit;

namespace Document.UnitTest.Mapper;

public class AutoMapperTest
{
	public readonly AutoMocker _mocker;
	private static IMapper _mapper;

	public AutoMapperTest()
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
	public void AutoMapperDocument()
	{
		var documentFaker = new DocumentFaker();
		var document = documentFaker.document;

		var result = _mapper.Map<DocumentModel>(document);

		result.ShouldNotBeNull();
		result.ShouldSatisfyAllConditions(
			() => result.Number.ShouldBe(document.Number),
			() => result.Date.ShouldBe(document.Date),
			() => result.Description.ShouldBe(document.Description),
			() => result.Operation.ShouldBe(document.Operation),
			() => result.Observation.ShouldBe(document.Observation),
			() => result.PaymentDate.ShouldBe(document.PaymentDate),
			() => result.DocumentType.ShouldBe(document.DocumentType),
			() => result.Paid.ShouldBe(document.Paid),
			() => result.Total.ShouldBe(document.Total)
		);
	}

	[Fact]
	public void AutoMapperPageDocument()
	{
		var config = new MapperConfiguration(cfg => cfg.AddProfile<PagesDocumentMapper>());
		config.AssertConfigurationIsValid();
	}
}