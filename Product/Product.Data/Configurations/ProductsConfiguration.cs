using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Models;

namespace Product.Data.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Products>
{
	public void Configure(EntityTypeBuilder<Products> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id).IsRequired();
		builder.Property(p => p.Code);
		builder.Property(p => p.Description).IsRequired();
		builder.Property(p => p.Category).IsRequired();
		builder.Property(p => p.NCM);
		builder.Property(p => p.GTIN).IsRequired();
		builder.Property(p => p.QRCode);
	}
}
