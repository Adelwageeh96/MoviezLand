using Microsoft.EntityFrameworkCore;
using MoviezLand.Core.IRepositories;
using MoviezLand.Core.IRepository;
using MoviezLand.Core.Models;
using MoviezLand.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.EF.Repositories
{
    internal class MovieRepository : BaseRepository<Movie>,IMovieRepository
    {
        public MovieRepository(ApplicationDbContext options): base(options) { }

        public Movie GetMovieWitGenres(int id) => context.Movies.Include(m => m.MovieGenres).SingleOrDefault(m=>m.Id==id);
    }
}
