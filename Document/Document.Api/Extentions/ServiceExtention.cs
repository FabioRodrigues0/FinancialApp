using Document.Application.Application;
using Document.Application.Application.Interface;
using Document.Application.Services;
using Document.Application.Services.Interface;
using Document.Data.Repositories;
using Document.Data.Repositories.Interfaces;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;

namespace Document.Api.Extentions;

public static class ServiceExtention
{
	public static void AddService(this IServiceCollection services)
	{
		services.AddScoped<IApplicationDocumentService, ApplicationDocumentService>();
		services.AddScoped<IDocumentService, DocumentService>();
		services.AddScoped<IDocumentRepository, DocumentRepository>();
		services.AddScoped<IServiceContext, ServiceContext>();
	}
}
