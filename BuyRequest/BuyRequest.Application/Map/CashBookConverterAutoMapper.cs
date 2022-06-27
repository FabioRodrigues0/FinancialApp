using AutoMapper;
using BuyRequest.Domain.Models;
using CashBook.Application.DTO;
using Infrastructure.Shared.Enums;

namespace BuyRequest.Application.Map;

public class CashBookConverterAutoMapper : Profile
{
	public CashBookConverterAutoMapper()
	{
		CreateMap<(BuyRequests obj, TypeRequest t, decimal dif), CashBookDto>()
			.ForMember(to => to.Origin, from => from.MapFrom(x => Origin.BuyRequest))
			.ForMember(to => to.OriginId, from => from.MapFrom(x => x.obj.Id))
			.ForMember(to => to.Description, from => from.MapFrom(x => "Buy Request nº" + x.obj.Code))
			.ForMember(to => to.Type, from => from.MapFrom(x =>
				x.t == TypeRequest.Remove && x.obj.Status == Status.Finished ? StatusCashBook.Reversal
					: x.t == TypeRequest.Update || x.t == TypeRequest.Patch && x.obj.Status == Status.Finished ? StatusCashBook.Receivement
						: x.t == TypeRequest.Add && x.obj.Status == Status.Finished ? StatusCashBook.Payment : StatusCashBook.Payment))
			.ForMember(to => to.Valor, from => from.MapFrom(x =>
				x.t == TypeRequest.Remove && x.obj.Status == Status.Finished ? Math.Abs(x.obj.TotalValor)
						: x.t == TypeRequest.Update || x.t == TypeRequest.Patch && x.obj.Status == Status.Finished ? Math.Abs(x.dif)
							: x.t == TypeRequest.Add && x.obj.Status == Status.Finished ? Math.Abs(x.obj.TotalValor) * -1 : Math.Abs(x.obj.TotalValor) * -1))
			.ReverseMap();
	}
}
