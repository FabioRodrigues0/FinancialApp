using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Shared;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions options) : base(options)
	{
	}
}