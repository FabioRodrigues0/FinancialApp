using Infrastructure.Shared;
using Infrastructure.Shared.Interfaces;
using Product.Application.Application;
using Product.Application.Application.Interface;
using Product.Application.Services;
using Product.Application.Services.Interface;
using Product.Data.Repositories;
using Product.Data.Repositories.Interfaces;

namespace Product.Api.Extentions;

public static class ServiceExtention
{
	public static void AddService(this IServiceCollection services)
	{
		services.AddScoped<IApplicationProductsService, ApplicationProductsService>();
		services.AddScoped<IProductsService, ProductsService>();
		services.AddScoped<IProductsRepository, ProductsRepository>();
		services.AddScoped<IServiceContext, ServiceContext>();
	}
}