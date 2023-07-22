using MoviezLand.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviezLand.Web.ViewModels.Genres
{
	public class GenreFormViewModel
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		public IEnumerable<string>? Movies { get; set; }
	}
}
