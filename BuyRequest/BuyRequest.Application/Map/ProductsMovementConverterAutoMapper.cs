using AutoMapper;
using BuyRequest.Domain.Entities;
using Stock.Application.DTO;

namespace BuyRequest.Application.Map
{
	public class ProductsMovementConverterAutoMapper : Profile
	{
		public ProductsMovementConverterAutoMapper()
		{
			CreateMap<BuyRequestProducts, ProductsMovementDto>()
				.ForMember(to => to.ProductId, from => from.MapFrom(x => x.Id))
				.ForMember(to => to.ProductDescription, from => from.MapFrom(x => x.ProductDescription))
				.ForMember(to => to.ProductCategory, from => from.MapFrom(x => x.ProductCategory))
				.ForMember(to => to.Quantity, from => from.MapFrom(x => x.Quantity))
				.ForMember(to => to.ValorPerUnit, from => from.MapFrom(x => x.Valor))
				.ForMember(to => to.StorageId, from => from.MapFrom(x => Guid.NewGuid()))
				.ForMember(to => to.StorageDescription, from => from.MapFrom(x => "Storage nº"+x.Total))
				.ReverseMap();

			//CreateMap<List<BuyRequestProducts>, List<ProductsMovementDto>>().ReverseMap();
		}
	}
}
