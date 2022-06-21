global using AutoMapper;
using BuyRequest.Api.Extentions;
using BuyRequest.Data;
using BuyRequest.Domain.Models;
using BuyRequest.Domain.Models.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Shared.Messaging.Settings;
using MessageBroker.Settings.Queue;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(fv =>
{
	fv.RegisterValidatorsFromAssemblyContaining<BuyRequestValidations>();
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddTransient<IValidator<BuyRequests>, BuyRequestValidations>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BuyRequestContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
		.UseLoggerFactory(ConsoleLoggerFactory)
		.EnableSensitiveDataLogging();
});

builder.Services.AddHttpClient();
builder.Services.AddService();
builder.Services.AddMapper();
builder.Services.AddListenerConfiguration(builder.Configuration);
builder.Services.AddPublisher();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
