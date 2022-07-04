using AutoMapper;
using CashBook.Application.Models;
using CashBook.Domain.Entities;

namespace CashBook.Application.Map;

public class CashBookAutoMapper : Profile
{
	public CashBookAutoMapper()
	{
		CreateMap<CashBooks, CashBookModel>().ReverseMap();
		CreateMap<CashBooks, CashBookUpdateModel>().ReverseMap();
	}
}
