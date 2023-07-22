﻿using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Genres;

namespace MoviezLand.Web.Controllers
{
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
		public IActionResult Add(GenreFormViewModel model)
		{
			if(ModelState.IsValid)
			{
				if (!unitOfWork.Genres.IsExist(model.Name))
				{
					unitOfWork.Genres.Add(new Genre { Name = model.Name });
					unitOfWork.Complete();
				}
				else
					ModelState.AddModelError("Name", "Genre already exist");
			}
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Edit(int id)
		{
			var genre = unitOfWork.Genres.FindById(id);
			if(genre is not null)
			{
				return PartialView("_GenreForm", new GenreFormViewModel {Name = genre.Name});
			}
			return NotFound();
		}
	}
}