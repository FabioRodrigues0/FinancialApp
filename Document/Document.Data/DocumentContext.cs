using Document.Data.Configurations;
using Document.Domain.Models;
using Infrastructure.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Document.Data;

public class DocumentContext : DataContext
{
	public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
	{
	}

	public DbSet<Documents> Documents { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new DocumentConfiguration()).Entity<Documents>();
	}
}
