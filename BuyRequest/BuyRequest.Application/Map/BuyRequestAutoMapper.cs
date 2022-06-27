using AutoMapper;
using BuyRequest.Application.DTO;
using BuyRequest.Domain.Models;

namespace BuyRequest.Application.Map;

public class BuyRequestAutoMapper : Profile
{
	public BuyRequestAutoMapper()
	{
		CreateMap<BuyRequestDto, BuyRequests>()
			.ForMember(to => to.ProductValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.Cost, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.TotalValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor) - x.Discount))
			.ReverseMap();
		CreateMap<BuyRequestUpdateDto, BuyRequests>()
			.ForMember(to => to.ProductValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.Cost, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor)))
			.ForMember(to => to.TotalValor, from => from.MapFrom(x => x.Products.Sum(x => x.Quantity * x.Valor) - x.Discount))
			.ReverseMap();
		CreateMap<BuyRequestPatchDto, BuyRequests>()
			.ForMember(to => to.ProductValor, from => from.UseDestinationValue())
			.ForMember(to => to.Cost, from => from.UseDestinationValue())
			.ForMember(to => to.TotalValor, from => from.UseDestinationValue())
			.ReverseMap();
		CreateMap<(List<BuyRequests> list, int totalPages, int page), (List<BuyRequestDto> list, int totalPages, int page)>().ReverseMap();
	}
}
