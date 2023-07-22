using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core;

namespace MoviezLand.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public MoviesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index() => View(unitOfWork.Movies.GetAll());
    }
}
