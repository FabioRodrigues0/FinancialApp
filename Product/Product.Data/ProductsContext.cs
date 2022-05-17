using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Product.Data.Configurations;
using Product.Domain.Models;

namespace Product.Data;

public class ProductsContext : DataContext
{
	public static readonly ILoggerFactory ConsoleLoggerFactory
			= LoggerFactory.Create(builder => { builder.AddDebug(); });

	public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
	{
	}

	public DbSet<Products> Products { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ProductsConfiguration()).Entity<Products>();
	}
}