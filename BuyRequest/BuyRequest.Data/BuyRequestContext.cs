using BuyRequest.Data.Configurations;
using BuyRequest.Domain.Entities;
using Infrastructure.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace BuyRequest.Data;

public class BuyRequestContext : DataContext
{
	public BuyRequestContext(DbContextOptions<BuyRequestContext> options) : base(options)
	{
	}

	public DbSet<BuyRequests> BuyRequests { get; set; }
	public DbSet<BuyRequestProducts> BuyRequestProducts { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new BuyRequestConfiguration()).Entity<BuyRequests>();
		modelBuilder.ApplyConfiguration(new BuyRequestProductConfiguration()).Entity<BuyRequestProducts>();
	}
}
