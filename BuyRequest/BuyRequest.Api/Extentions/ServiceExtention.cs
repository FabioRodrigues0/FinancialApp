﻿using BuyRequest.Application.Application;
using BuyRequest.Application.Application.Interface;
using BuyRequest.Application.Services;
using BuyRequest.Application.Services.Interfaces;
using BuyRequest.Data.Repositories;
using BuyRequest.Data.Repositories.Interfaces;
using CashBook.ApiClient;
using CashBook.ApiClient.Interface;
using Infrastructure.Shared;
using Infrastructure.Shared.Interfaces;

namespace BuyRequest.Api.Extentions;

public static class ServiceExtention
{
	public static void AddService(this IServiceCollection services)
	{
		services.AddScoped<IApplicationBuyRequestService, ApplicationBuyRequestService>();
		services.AddScoped<IBuyRequestService, BuyRequestService>();
		services.AddScoped<IBuyRequestProductService, BuyRequestProductService>();
		services.AddScoped<IBuyRequestProductsRepository, BuyRequestProductsRepository>();
		services.AddScoped<IBuyRequestRepository, BuyRequestRepository>();
		services.AddScoped<IServiceContext, ServiceContext>();
		services.AddScoped<ICashBookApiClient, CashBookApiClient>();
	}
}