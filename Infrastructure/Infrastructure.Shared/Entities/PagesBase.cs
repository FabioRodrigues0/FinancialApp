namespace Infrastructure.Shared.Entities
{
	public class PagesBase<T> where T : class
	{
		public List<T> Models { get; set; }
		public int CurrentPage { get; set; }
		public int Pages { get; set; }
	}
}
