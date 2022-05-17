namespace Product.Application.DTO;

public class PagesProductsDto
{
	public List<ProductsDto> Models { get; set; }
	public int CurrentPage { get; set; }
	public int Pages { get; set; }
}