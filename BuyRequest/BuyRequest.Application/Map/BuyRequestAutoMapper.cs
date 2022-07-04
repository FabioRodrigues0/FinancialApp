using AutoMapper;
using BuyRequest.Application.Models;
using BuyRequest.Domain.Entities;

namespace BuyRequest.Application.Map;

public class BuyRequestAutoMapper : Profile
{
	public BuyRequestAutoMapper()
	{
		CreateMap<BuyRequestModel, BuyRequests>()
			.ForMember(to => to.ProductValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.Cost, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.TotalValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor) - x.Discount))
			.ReverseMap();
		CreateMap<BuyRequestUpdateModel, BuyRequests>()
			.ForMember(to => to.ProductValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.Cost, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.TotalValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor) - x.Discount))
			.ReverseMap();
		CreateMap<BuyRequestPatchModel, BuyRequests>()
			.ForMember(to => to.ProductValor, from => from.UseDestinationValue())
			.ForMember(to => to.Cost, from => from.UseDestinationValue())
			.ForMember(to => to.TotalValor, from => from.UseDestinationValue())
			.ReverseMap();
	}
}
