using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core;
using MoviezLand.Core.Constants;
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

        public IActionResult Add()
        {
            return View(new MovieFormViewModel
            {
                Genres = unitOfWork.Genres.GetAll(g => g.Name, OrderBy.Ascending)
            }) ;
        }
        
    }
}
