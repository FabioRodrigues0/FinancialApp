using AutoMapper;
using Infrastructure.Shared.Entities;
using Product.Application.Models;
using Product.Domain.Entities;

namespace Product.Application.Map;

public class PagesProductsMapper : Profile
{
	public PagesProductsMapper()
	{
		CreateMap<PagesBase<Products>, PagesGetAllProductsModel>().ReverseMap();
	}
}
