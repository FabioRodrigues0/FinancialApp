using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}
	}
}
