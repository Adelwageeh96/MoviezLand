using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviezLand.Core;
using MoviezLand.Core.Constants;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Genres;

namespace MoviezLand.Web.Controllers
{
	[Authorize(Roles =$"{Role.Admin},{Role.Manager}")]
	public class GenresController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		public GenresController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var model = unitOfWork.Genres.GetAll().Select(g => new GenreFormViewModel
			{
				Id=g.Id,
				Name = g.Name,
				Movies = unitOfWork.Genres.GenreMovies(g.Id)

			});
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(GenreFormViewModel model)
		{
			if(ModelState.IsValid)
			{
				if (!unitOfWork.Genres.IsExist(model.Name))
				{
					unitOfWork.Genres.Add(new Genre { Name = model.Name });
					unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
				else
					ModelState.AddModelError("Name", "Genre already exist");
			}
            var genremodel = unitOfWork.Genres.GetAll().Select(g => new GenreFormViewModel
            {
                Id = g.Id,
                Name = g.Name,
                Movies = unitOfWork.Genres.GenreMovies(g.Id)

            });
            return View(nameof(Index), genremodel);

        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(GenreFormViewModel model)
		{
			var genre = unitOfWork.Genres.FindById(model.Id);
			if (genre != null)
			{
				if (!unitOfWork.Genres.IsExist(model.Name))
				{
					genre.Name = model.Name;
					unitOfWork.Complete();
					return RedirectToAction(nameof(Index));

				}
				else
					ModelState.AddModelError("Name", "Genre already exist");
            }
			else
				return NotFound();
            var genremodel = unitOfWork.Genres.GetAll().Select(g => new GenreFormViewModel
            {
                Id = g.Id,
                Name = g.Name,
                Movies = unitOfWork.Genres.GenreMovies(g.Id)

            });
            return View(nameof(Index),genremodel);
        }

	}
}
