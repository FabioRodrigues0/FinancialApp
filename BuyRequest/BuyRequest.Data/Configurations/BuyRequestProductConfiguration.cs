using BuyRequest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuyRequest.Data.Configurations;

public class BuyRequestProductConfiguration : IEntityTypeConfiguration<BuyRequestProducts>
{
	public void Configure(EntityTypeBuilder<BuyRequestProducts> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id).IsRequired();
		builder.Property(p => p.BuyRequestId).IsRequired();
		builder.Property(p => p.ProductId).IsRequired();
		builder.Property(p => p.ProductDescription).IsRequired();
		builder.Property(p => p.ProductCategory).IsRequired();
		builder.Property(p => p.Quantity)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
		builder.Property(p => p.Valor)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
		builder.Property(p => p.Total)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
	}
}