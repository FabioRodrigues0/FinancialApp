using AutoMapper;
using Product.Application.DTO;
using Product.Domain.Models;

namespace Product.Application.Map;

public class ProductsAutoMapper : Profile
{
	public ProductsAutoMapper()
	{
		CreateMap<Products, ProductsDto>().ReverseMap();
		CreateMap<Products, ProductsWithIdDto>().ReverseMap();
		CreateMap<Products, ProductsCategoryDto>().ReverseMap();
		CreateMap<(List<Products> list, int totalPages, int page), (List<ProductsWithIdDto> list, int totalPages, int page)>().ReverseMap();
		CreateMap<(List<Products> list, int totalPages, int page), (List<ProductsCategoryDto> list, int totalPages, int page)>().ReverseMap();
	}
}