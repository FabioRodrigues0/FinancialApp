using AutoMapper;
using CashBook.Application.DTO;
using Document.Domain.Models;
using Infrastructure.Shared.Enums;

namespace Document.Application.Map;

public class CashBookConverterAutoMapper : Profile
{
	public CashBookConverterAutoMapper()
	{
		CreateMap<(Documents obj, TypeRequest t, decimal dif), CashBookDto>()
			.ForMember(to => to.Origin, from => from.MapFrom(x => Origin.Document))
			.ForMember(to => to.OriginId, from => from.MapFrom(x => x.obj.Id))
			.ForMember(to => to.Description, from => from.MapFrom(x => "Document nº" + x.obj.Number))
			.ForMember(to => to.Type, from => from.MapFrom(x =>
				x.t == TypeRequest.Remove && x.obj.Paid == true ? StatusCashBook.Reversal
					: x.t == TypeRequest.Update || x.t == TypeRequest.Patch && x.obj.Paid == true ? StatusCashBook.Receivement
						: x.t == TypeRequest.Add && x.obj.Paid == true && x.obj.Operation == Operation.Input ? StatusCashBook.Payment : StatusCashBook.Receivement))
			.ForMember(to => to.Valor, from => from.MapFrom(x =>
				x.t == TypeRequest.Remove && x.obj.Paid == true ? Math.Abs(x.obj.Total)
					: x.t == TypeRequest.Update || x.t == TypeRequest.Patch && x.obj.Paid == true ? Math.Abs(x.dif)
						: x.t == TypeRequest.Add && x.obj.Paid == true && x.obj.Operation == Operation.Input ? Math.Abs(x.obj.Total) : Math.Abs(x.obj.Total) * -1))
			.ReverseMap();
	}
}
