using AutoMapper;
using BuyRequest.Domain.Models;
using CashBook.Application.DTO;
using Infrastructure.Shared.Enums;
using Stock.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
