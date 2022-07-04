using AutoMapper;
using CashBook.Application.Models;
using CashBook.Domain.Entities;
using Infrastructure.Shared.Entities;

namespace CashBook.Application.Map;

public class PagesCashBookMapper : Profile
{
	public PagesCashBookMapper()
	{
		CreateMap<PagesBase<CashBooks>, PagesCashBookModel>()
				.ForMember(to => to.Models, from => from.MapFrom(x => x.Models))
				.ForMember(to => to.CurrentPage, from => from.MapFrom(x => x.CurrentPage))
				.ForMember(to => to.Pages, from => from.MapFrom(x => x.Pages))
				.ForMember(to => to.Total, from => from.MapFrom(x => x.Models.Sum(y => y.Valor)));
	}
}
