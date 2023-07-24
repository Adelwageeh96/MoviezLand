using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core;
using MoviezLand.Core.Constants;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Movies;

namespace MoviezLand.Web.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public MoviesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index() => View(unitOfWork.Movies.GetAll());

        [Authorize(Roles =$"{Role.Admin},{Role.Manager}")]
        public IActionResult Add()
        {
            var model = new  MovieFormViewModel
            {
                Genres = unitOfWork.Genres.GetAll(g => g.Name, OrderBy.Ascending)
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                var files = Request.Form.Files;
                var poster = files.FirstOrDefault();
                var allowedExtenstions = new List<string> { ".jpg", ".png" };
                if (poster != null)
                {
                    if (allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                    {
                        if (poster.Length <= 1048576)
                        {
                            using var dataStream = new MemoryStream();
                            await poster.CopyToAsync(dataStream);
                            var newMovie = new Movie
                            {
                                Title = model.Title,
                                Story = model.Story,
                                Year = model.Year,
                                Poster = dataStream.ToArray(),
                            };

                            unitOfWork.Movies.Add(newMovie);
                            unitOfWork.Complete();
                            model.Id = newMovie.Id;
                            if(model.SelectedGenreIds is not null && model.SelectedGenreIds.Any())
                            {
                                var selectedGenres = unitOfWork.Genres.GetAll().Where(g => model.SelectedGenreIds.Contains(g.Id)).ToList();
                                foreach (var genre in selectedGenres)
                                {
                                    unitOfWork.MoviesGenres.Add(new MovieGenre
                                    {
                                        MovieId=model.Id,
                                        GenreId=genre.Id
                                    });
                                }
                                unitOfWork.Complete();
                            }
                            return RedirectToAction(nameof(Index));
                        }
                        else
                            ModelState.AddModelError("Poster", "Poster can't be more than 1MB!");
                    }
                    else
                        ModelState.AddModelError("Poster", "Only .jpg, .png images are allowed");
                }
                else
                    ModelState.AddModelError("Poster", "Please select movie poster!");
            }
            return View(new MovieFormViewModel
            {
                Genres= unitOfWork.Genres.GetAll(g=>g.Name,OrderBy.Ascending)
            });
        }
        
    } 
}
