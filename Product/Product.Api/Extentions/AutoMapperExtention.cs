using AutoMapper;
using Product.Application.Map;

namespace Product.Api.Extentions;

public static class AutoMapperExtention
{
	public static void AddMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(Program));
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new ProductsAutoMapper());
			cfg.AddProfile(new PagesProductsMapper());
		});
		var mapper = config.CreateMapper();

		services.AddSingleton(mapper);
	}
}