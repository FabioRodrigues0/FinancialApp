using CashBook.Application.Application;
using CashBook.Application.Application.Interface;
using CashBook.Application.Services;
using CashBook.Application.Services.Interface;
using CashBook.Data.Repositories;
using CashBook.Data.Repositories.Interfaces;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Interface;

namespace CashBook.Api.Extentions;

public static class ServiceExtention
{
	public static void AddService(this IServiceCollection services)
	{
		services.AddScoped<IApplicationCashBookService, ApplicationCashBookService>();
		services.AddScoped<ICashBookService, CashBookService>();
		services.AddScoped<ICashBookRepository, CashBookRepository>();
		services.AddScoped<IServiceContext, ServiceContext>();
	}
}
