namespace Product.Application.DTO;

public class PagesGetCategoryProductsDto
{
	public List<ProductsCategoryDto> Models { get; set; }
	public int CurrentPage { get; set; }
	public int Pages { get; set; }
}