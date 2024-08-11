namespace Admin.Services
{
	public class PageServices
	{
		public string CurrentPage { get; set; }

		public void ChangePage(string newPage)
		{
			CurrentPage = newPage;
		}
	}
}
