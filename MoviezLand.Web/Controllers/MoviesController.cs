﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core;
using MoviezLand.Core.Constants;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Movies;
using MoviezLand.Web.ViewModels.Shared;
using MoviezLand.Web.ViewModels.Reviews;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MoviezLand.Web.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IToastNotification toastNotification;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public MoviesController(IUnitOfWork unitOfWork , IToastNotification toastNotification,UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.toastNotification = toastNotification;
            this.userManager = userManager;
            this.httpContextAccessor= httpContextAccessor;
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
                            toastNotification.AddInfoToastMessage("Movie created successfuly");
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

        [Authorize(Roles = $"{Role.Admin},{Role.Manager}")]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var movie= unitOfWork.Movies.FindById((int)id);
            if(movie is not null)
            {
               return View("Add",new MovieFormViewModel
               {
                   Id=movie.Id,
                   Title=movie.Title,
                   Story=movie.Story,
                   Poster=movie.Poster,
                   Year=movie.Year,
                   Genres = unitOfWork.Genres.GetAll(),
                   SelectedGenreIds =unitOfWork.MoviesGenres.GetAll()
                                                            .Where(mg=>mg.MovieId==movie.Id)
                                                            .Select(mg=>mg.GenreId).ToList()
               });
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id,MovieFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = unitOfWork.Movies.FindById(id);
                if(movie is not null)
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
                                movie.Poster = dataStream.ToArray();
                            }
                            else
                            {
                                model.Genres = unitOfWork.Genres.GetAll();
                                ModelState.AddModelError("Poster", "Poster can't be more than 1MB!");
                                return View("Add", model);
                            }
                        }
                        else
                        {
                            model.Genres = unitOfWork.Genres.GetAll();
                            ModelState.AddModelError("Poster", "Only .jpg, .png images are allowed");
                            return View("Add", model);
                        }
                    }
                    movie.Title = model.Title;
                    movie.Year = model.Year;
                    movie.Story = model.Story;
                    var oldMovieGenres = unitOfWork.MoviesGenres.GetAll().Where(mg => mg.MovieId == id);
                    unitOfWork.MoviesGenres.RemoveRange(oldMovieGenres);
                    unitOfWork.Complete();
                    if (model.SelectedGenreIds is not null && model.SelectedGenreIds.Any())
                    {
                        foreach (var genreId in model.SelectedGenreIds)
                        {
                            unitOfWork.MoviesGenres.Add(new MovieGenre
                            {
                                MovieId = model.Id,
                                GenreId = genreId
                            });
                        }
                        unitOfWork.Complete();
                    }
                    toastNotification.AddInfoToastMessage("Movie updated successfuly");
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            model.Genres = unitOfWork.Genres.GetAll();
            return View("Add", model);
        }

        public async  Task<IActionResult> Review([FromRoute] int? id, [FromRoute] bool isInEditMode=false)
        {
            if(id is not null)
            {
                var movie = unitOfWork.Movies.GetAll().SingleOrDefault(m=>m.Id== id);
                var genresForMovie= unitOfWork.MoviesGenres.GetAll().Where(mg=>mg.MovieId==id).Select(mg=>mg.GenreId).ToList();
                List<string> genres = new List<string>();
                foreach (var genreid in genresForMovie)
                {
                    genres.Add(unitOfWork.Genres.GetAll().Where(g=>g.Id==genreid).Select(g=>g.Name).First());
                }
                ViewData["GenresNames"] = genres;
                if (movie is not null)
                {
                    var reviews = unitOfWork.Reviews.GetAll().Where(r => r.MovieId == id).ToList();
                    var reviewFormViewModels = new List<ReviewFormViewModel>();
                    var curUser = httpContextAccessor.HttpContext.User;

                    foreach (var review in reviews)
                    {
                        var user = await userManager.FindByIdAsync(review.UserId);


                        reviewFormViewModels.Add(new ReviewFormViewModel
                        {
                            MovieId = review.MovieId,
                            Content = review.Content,
                            UserId = review.UserId,
                            Id = review.Id,
                            UserName = user.UserName,
                            UserProfile = user.ProfilePicture,
                            CurrentUser= curUser.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                            IsInEditMode= isInEditMode
                        });
                    }
                    var userDetails = await userManager.FindByIdAsync(curUser.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    var model = new AddEditReviewFormViewModel()
                    {
                        UserName= userDetails.UserName,
                        ProfilePicture = userDetails.ProfilePicture,
                        UserId= curUser.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                        MovieId=(int) id 
                    };

                    return View(new MovieWithReviewsViewModel
                    {
                        Movie = movie,
                        Reviews = reviewFormViewModels,
                        AddEditReviewFormViewModel= model

                    });

                }
                return NotFound();

            }
            return BadRequest();
        }

        public IActionResult Delete(int? id)
        {
            if(id is not null)
            {
                var movie = unitOfWork.Movies.FindById((int)id);
                if(movie is not null)
                {
                    var reviewsForMovie = unitOfWork.Reviews.GetAll().Where(r => r.MovieId == id);
                    unitOfWork.Reviews.RemoveRange(reviewsForMovie);
                    unitOfWork.Complete();
                    unitOfWork.Movies.Remove(movie);
                    unitOfWork.Complete();

                    return RedirectToAction("Index");   
                }
                return NotFound();

            }
            return BadRequest();
        }


    }
}
