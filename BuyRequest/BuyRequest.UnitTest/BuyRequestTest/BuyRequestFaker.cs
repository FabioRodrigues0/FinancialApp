using Bogus;
using BuyRequest.Application.DTO;
using BuyRequest.Domain.Models;
using Infrastructure.Shared.Enums;
using System;
using System.Collections.Generic;

namespace BuyRequest.UnitTest.BuyRequestTest;

public class BuyRequestFaker
{
	public static Faker<BuyRequestProducts> buyRequestProducts = new Faker<BuyRequestProducts>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.ProductId, Guid.NewGuid)
		.RuleFor(x => x.ProductDescription, x => x.Random.String(1, 256))
		.RuleFor(x => x.ProductCategory, x => x.PickRandom<ProductCategory>())
		.RuleFor(x => x.Quantity, x => x.Random.Int(1, 3))
		.RuleFor(x => x.Valor, x => x.Random.Decimal(1, 30));

	public static Faker<BuyRequestProductDto> buyRequestProductsDto = new Faker<BuyRequestProductDto>()
		.RuleFor(x => x.ProductDescription, x => x.Random.String(1, 256))
		.RuleFor(x => x.ProductCategory, x => x.PickRandom<ProductCategory>())
		.RuleFor(x => x.Quantity, x => x.Random.Int(1, 3))
		.RuleFor(x => x.Valor, x => x.Random.Decimal(1, 30));

	public static Faker<BuyRequestDto> buyRequestFaker = new Faker<BuyRequestDto>()
		.RuleFor(x => x.Code, x => x.Random.Number(1, 50000))
		.RuleFor(x => x.Date, DateTimeOffset.Now)
		.RuleFor(x => x.DeliveryDate, DateTimeOffset.Now.AddDays(6))
		.RuleFor(x => x.Client, Guid.NewGuid)
		.RuleFor(x => x.ClientDescription, x => x.Random.String(1, 256))
		.RuleFor(x => x.ClientEmail, x => x.Person.Email)
		.RuleFor(x => x.ClientPhone, x => x.Person.Phone)
		.RuleFor(x => x.Status, x => x.PickRandom<Status>())
		.RuleFor(x => x.Discount, 0)
		.RuleFor(x => x.Cost, 0)
		.RuleFor(x => x.Products, x => buyRequestProductsDto.GenerateBetween(1, 3));

	public BuyRequests buyRequest = new Faker<BuyRequests>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Code, x => x.Random.Number(1, 50000))
		.RuleFor(x => x.Date, DateTimeOffset.Now)
		.RuleFor(x => x.DeliveryDate, DateTimeOffset.Now.AddDays(6))
		.RuleFor(x => x.Client, Guid.NewGuid)
		.RuleFor(x => x.ClientDescription, x => x.Random.String(1, 256))
		.RuleFor(x => x.ClientEmail, x => x.Person.Email)
		.RuleFor(x => x.ClientPhone, x => x.Person.Phone)
		.RuleFor(x => x.Status, x => x.PickRandom<Status>())
		.RuleFor(x => x.Discount, 0)
		.RuleFor(x => x.Cost, 0)
		.RuleFor(x => x.ProductValor, 0)
		.RuleFor(x => x.TotalValor, 0)
		.RuleFor(x => x.Products, x => buyRequestProducts.GenerateBetween(1, 3));

	public PagesBuyRequestDto pageBuyRequest = new Faker<PagesBuyRequestDto>()
		.RuleFor(x => x.Models, x => buyRequestFaker.GenerateBetween(1, 3))
		.RuleFor(x => x.CurrentPage, 1)
		.RuleFor(x => x.Pages, 1);

	public List<BuyRequestDto> listDto = new Faker<BuyRequestDto>()
		.RuleFor(x => x.Code, x => x.Random.Number(1, 50000))
		.RuleFor(x => x.Date, DateTimeOffset.Now)
		.RuleFor(x => x.DeliveryDate, DateTimeOffset.Now.AddDays(6))
		.RuleFor(x => x.Client, Guid.NewGuid)
		.RuleFor(x => x.ClientDescription, x => x.Random.String(1, 256))
		.RuleFor(x => x.ClientEmail, x => x.Person.Email)
		.RuleFor(x => x.ClientPhone, x => x.Person.Phone)
		.RuleFor(x => x.Status, x => x.PickRandom<Status>())
		.RuleFor(x => x.Discount, 0)
		.RuleFor(x => x.Cost, 0)
		.RuleFor(x => x.Products, x => buyRequestProductsDto.GenerateBetween(1, 3))
		.GenerateBetween(1, 3);

	public List<BuyRequests> listModel = new Faker<BuyRequests>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Code, x => x.Random.Number(1, 50000))
		.RuleFor(x => x.Date, DateTimeOffset.Now)
		.RuleFor(x => x.DeliveryDate, DateTimeOffset.Now.AddDays(6))
		.RuleFor(x => x.Client, Guid.NewGuid)
		.RuleFor(x => x.ClientDescription, x => x.Random.String(1, 256))
		.RuleFor(x => x.ClientEmail, x => x.Person.Email)
		.RuleFor(x => x.ClientPhone, x => x.Person.Phone)
		.RuleFor(x => x.Status, x => x.PickRandom<Status>())
		.RuleFor(x => x.Discount, 0)
		.RuleFor(x => x.Cost, 0)
		.RuleFor(x => x.ProductValor, 0)
		.RuleFor(x => x.TotalValor, 0)
		.RuleFor(x => x.Products, x => buyRequestProducts.GenerateBetween(1, 3))
		.GenerateBetween(1, 3);
}
