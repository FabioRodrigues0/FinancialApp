using Bogus;
using Infrastructure.Shared.Enums;
using Product.Application.Models;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Product.UnitTest.ProductTest;

public class ProductsFaker
{
	public Products products = new Faker<Products>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Code, x => x.Random.String(1, 10))
		.RuleFor(x => x.Category, x => x.Random.Enum<ProductCategory>())
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.NCM, x => x.Random.String(1, 256))
		.RuleFor(x => x.GTIN, x => x.Random.String(1, 256))
		.RuleFor(x => x.QRCode, x => x.Random.String(1, 256));

	public ProductsModel productsDto = new Faker<ProductsModel>()
		.RuleFor(x => x.Code, x => x.Random.String(1, 10))
		.RuleFor(x => x.Category, x => x.Random.Enum<ProductCategory>())
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.NCM, x => x.Random.String(1, 256))
		.RuleFor(x => x.GTIN, x => x.Random.String(1, 256))
		.RuleFor(x => x.QRCode, x => x.Random.String(1, 256));

	public List<Products> list = new Faker<Products>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Code, x => x.Random.String(1, 10))
		.RuleFor(x => x.Category, x => x.Random.Enum<ProductCategory>())
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.NCM, x => x.Random.String(1, 256))
		.RuleFor(x => x.GTIN, x => x.Random.String(1, 256))
		.RuleFor(x => x.QRCode, x => x.Random.String(1, 256))
		.GenerateBetween(1, 3);

	public List<ProductsWithIdModel> listWithId = new Faker<ProductsWithIdModel>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Code, x => x.Random.String(1, 10))
		.RuleFor(x => x.Category, x => x.Random.Enum<ProductCategory>())
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.RuleFor(x => x.NCM, x => x.Random.String(1, 256))
		.RuleFor(x => x.GTIN, x => x.Random.String(1, 256))
		.RuleFor(x => x.QRCode, x => x.Random.String(1, 256))
		.GenerateBetween(1, 3);

	public List<ProductsCategoryModel> listCategory = new Faker<ProductsCategoryModel>()
		.RuleFor(x => x.Id, Guid.NewGuid)
		.RuleFor(x => x.Category, x => x.Random.Enum<ProductCategory>())
		.RuleFor(x => x.Description, x => x.Random.String(1, 256))
		.GenerateBetween(1, 3);
}
