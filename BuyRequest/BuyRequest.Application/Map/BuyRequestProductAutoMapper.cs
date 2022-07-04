using AutoMapper;
using BuyRequest.Application.Models;
using BuyRequest.Domain.Entities;

namespace BuyRequest.Application.Map;

public class BuyRequestProductAutoMapper : Profile
{
	public BuyRequestProductAutoMapper()
	{
		CreateMap<BuyRequestProductModel, BuyRequestProducts>()
			.ForMember(to => to.Total, from => from.MapFrom(x => x.Valor * x.Quantity))
			.ReverseMap();
		CreateMap<BuyRequestProductUpdateModel, BuyRequestProducts>()
			.ForMember(to => to.Total, from => from.MapFrom(x => x.Valor * x.Quantity))
			.ReverseMap();
	}
}