using Infrastructure.Shared.Entities;

namespace CashBook.Application.Models;

public class PagesCashBookModel : PagesBase<CashBookModel>
{
	public decimal Total { get; set; }
}
