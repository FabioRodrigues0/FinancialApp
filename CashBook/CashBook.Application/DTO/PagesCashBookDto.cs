using Infrastructure.Shared.Models;

namespace CashBook.Application.DTO;

public class PagesCashBookDto : PagesBase<CashBookDto>
{
	public decimal Total { get; set; }
}
