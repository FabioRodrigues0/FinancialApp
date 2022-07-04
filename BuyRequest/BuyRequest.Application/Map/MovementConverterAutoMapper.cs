using AutoMapper;
using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Enums;
using Stock.Application.DTO;

namespace BuyRequest.Application.Map
{
	public class MovementConverterAutoMapper : Profile
	{
		public MovementConverterAutoMapper()
		{
			CreateMap<BuyRequests, MovementsDto>()
				.ForMember(to => to.Origin, from => from.MapFrom(x => Origin.BuyRequest))
				.ForMember(to => to.OriginId, from => from.MapFrom(x => x.Id))
				.ForMember(to => to.Type, from => from.MapFrom(x => Operation.Output))
				.ForMember(to => to.ProductsMovements, from => from.MapFrom(x => x.Products))
				.ReverseMap();
		}
	}
}
