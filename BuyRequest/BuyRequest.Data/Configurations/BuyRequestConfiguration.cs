using BuyRequest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuyRequest.Data.Configurations;

public class BuyRequestConfiguration : IEntityTypeConfiguration<BuyRequests>
{
	public void Configure(EntityTypeBuilder<BuyRequests> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id).IsRequired();
		builder.Property(p => p.Code).IsRequired();
		builder.Property(p => p.Date).IsRequired();
		builder.Property(p => p.Client).IsRequired();
		builder.Property(p => p.ClientDescription).IsRequired();
		builder.Property(p => p.ClientEmail).IsRequired();
		builder.Property(p => p.ClientPhone).IsRequired();
		builder.Property(p => p.Status).IsRequired();
		builder.Property(p => p.Discount).HasColumnType("decimal(18,2)");
		builder.Property(p => p.ProductValor)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
		builder.Property(p => p.Cost)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
		builder.Property(p => p.TotalValor)
			.IsRequired()
			.HasColumnType("decimal(18,2)");

		builder.HasMany(w => w.Products)
			.WithOne(w => w.BuyRequest)
			.HasForeignKey(f => f.BuyRequestId)
			.HasConstraintName("FK_BuyRequestProducts_BuyRequestId_BuyRequest_Id");
	}
}