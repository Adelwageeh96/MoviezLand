namespace MoviezLand.Web.ViewModels.Reviews
{
	public class AddEditReviewFormViewModel
	{
		public int id { get; set; }

		public string? UserName { get; set; }

		public string UserId { get; set; }
		public byte[]? ProfilePicture { get; set; }

		public string Content { get; set; }

		public int MovieId { get; set; }

	}
}
