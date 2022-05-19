namespace Product.Application.DTO;

public class PagesGetAllProductsDto
{
	public List<ProductsWithIdDto> Models { get; set; }
	public int CurrentPage { get; set; }
	public int Pages { get; set; }
}