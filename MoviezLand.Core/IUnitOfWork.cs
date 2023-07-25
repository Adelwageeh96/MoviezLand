using MoviezLand.Core.IRepositories;
using MoviezLand.Core.IRepository;
using MoviezLand.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<MovieGenre> MoviesGenres { get; }

        public IBaseRepository<Review> Reviews { get; }
        public IGenreRepository Genres { get; }
        public IMovieRepository Movies { get; }
        int Complete();
    }
}
