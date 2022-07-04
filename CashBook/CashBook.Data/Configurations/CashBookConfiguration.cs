using CashBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBook.Data.Configurations;

public class CashBookConfiguration : IEntityTypeConfiguration<CashBooks>
{
	public void Configure(EntityTypeBuilder<CashBooks> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id).IsRequired();
		builder.Property(p => p.Origin).IsRequired();
		builder.Property(p => p.OriginId).IsRequired();
		builder.Property(p => p.Description).IsRequired();
		builder.Property(p => p.Type).IsRequired();
		builder.Property(p => p.Valor)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
	}
}