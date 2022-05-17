using AutoMapper;
using Product.Application.DTO;
using Product.Domain.Models;

namespace Product.Application.Map;

public class ProductsAutoMapper : Profile
{
	public ProductsAutoMapper()
	{
		CreateMap<Products, ProductsDto>().ReverseMap();
		CreateMap<Products, ProductsUpdateDto>().ReverseMap();
		CreateMap<Products, ProductsCategoryDto>().ReverseMap();
		CreateMap<(List<Products> list, int totalPages, int page), (List<ProductsDto> list, int totalPages, int page)>().ReverseMap();
	}
}