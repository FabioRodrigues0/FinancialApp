using CashBook.Data.Configurations;
using CashBook.Domain.Entities;
using Infrastructure.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace CashBook.Data;

public class CashBookContext : DataContext
{
	public CashBookContext(DbContextOptions<CashBookContext> options) : base(options)
	{
	}

	public DbSet<CashBooks> CashBooks { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new CashBookConfiguration()).Entity<CashBooks>();
	}
}
