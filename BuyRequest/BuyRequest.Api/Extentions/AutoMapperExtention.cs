using BuyRequest.Application.Map;

namespace BuyRequest.Api.Extentions;

public static class AutoMapperExtention
{
	public static void AddMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(Program));
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new BuyRequestAutoMapper());
			cfg.AddProfile(new BuyRequestProductAutoMapper());
			cfg.AddProfile(new PagesBuyRequestMapper());
			cfg.AddProfile(new CashBookConverterAutoMapper());
			cfg.AddProfile(new ProductsMovementConverterAutoMapper());
			cfg.AddProfile(new MovementConverterAutoMapper());
		});
		var mapper = config.CreateMapper();

		services.AddSingleton(mapper);
	}
}
