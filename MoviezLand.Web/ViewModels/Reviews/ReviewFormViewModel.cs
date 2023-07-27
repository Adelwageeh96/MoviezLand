namespace MoviezLand.Web.ViewModels.Reviews
{
	public class ReviewFormViewModel
	{
		public int Id { get; set; }

		public string Content { get; set; }

		public string UserId { get; set; }
		public string UserName { get; set; }
		
		public byte[] UserProfile { get; set; }

		public int MovieId { get; set; }

		public string CurrentUser { get; set; }

		public bool IsInEditMode { get; set; }
	}
}
