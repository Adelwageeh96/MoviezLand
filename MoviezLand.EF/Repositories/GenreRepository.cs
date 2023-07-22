using MoviezLand.Core.IRepositories;
using MoviezLand.Core.Models;
using MoviezLand.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.EF.Repositories
{
	public class GenreRepository :BaseRepository<Genre>,IGenreRepository
	{
		public GenreRepository(ApplicationDbContext context):base(context) { }

		public IEnumerable<string> GenreMovies(int id)=> context.Movies.Where(m=>m.MovieGenres.Any(mg=>mg.GenreId==id)).Select(m=>m.Title).ToList();

		public bool IsExist(string name) => context.Genres.Any(g => g.Name.ToUpper() == name.ToUpper());
	}
}
