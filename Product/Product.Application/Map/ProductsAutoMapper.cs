using AutoMapper;
using Product.Application.Models;
using Product.Domain.Entities;

namespace Product.Application.Map;

public class ProductsAutoMapper : Profile
{
	public ProductsAutoMapper()
	{
		CreateMap<Products, ProductsModel>().ReverseMap();
		CreateMap<Products, ProductsWithIdModel>().ReverseMap();
		CreateMap<Products, ProductsCategoryModel>().ReverseMap();
	}
}
