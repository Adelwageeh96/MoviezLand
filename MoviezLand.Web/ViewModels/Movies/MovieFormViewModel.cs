using MoviezLand.Core.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviezLand.Web.ViewModels.Movies
{
	public class MovieFormViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		[Required]
		[StringLength(2500)]
		public string Story { get; set; }
		public byte[] Poster { get; set; }
		[Required]
		public int Year { get; set; }
		[DisplayName("Genre")]
		public List<MovieGenre> MovieGenres { get; set; }

		public IEnumerable<Genre> Genres { get; set; } 
	}
}
