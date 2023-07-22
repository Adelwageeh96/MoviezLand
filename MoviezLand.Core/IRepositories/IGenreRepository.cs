using MoviezLand.Core.IRepository;
using MoviezLand.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core.IRepositories
{
	public interface IGenreRepository  : IBaseRepository<Genre>
	{
		IEnumerable<string> GenreMovies(int id);
		bool IsExist(string name);
	}
}
