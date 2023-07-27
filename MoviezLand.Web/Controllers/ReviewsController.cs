using Microsoft.AspNetCore.Mvc;
using MoviezLand.Core;
using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Reviews;

namespace MoviezLand.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public ReviewsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Add(AddEditReviewFormViewModel model)
        {
            if (ModelState.IsValid)
            {
				unitOfWork.Reviews.Add(new Review
				{
					Content = model.Content,
					MovieId = model.MovieId,
					UserId = model.UserId
				});
				unitOfWork.Complete();
			}
            return RedirectToAction("Review", "Movies", new {id=model.MovieId});
        }
        public IActionResult Delete([FromRoute] int id, int movieId)
        {
            var review= unitOfWork.Reviews.FindById(id);
            if(review != null)
            {
                unitOfWork.Reviews.Remove(review); 
                unitOfWork.Complete();
                return RedirectToAction("Review", "Movies", new { id = movieId });
            }
            return NotFound();
        }
    }
}
