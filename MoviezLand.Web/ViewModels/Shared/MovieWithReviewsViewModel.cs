using MoviezLand.Core.Models;
using MoviezLand.Web.ViewModels.Reviews;

namespace MoviezLand.Web.ViewModels.Shared
{
    public class MovieWithReviewsViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<ReviewFormViewModel> Reviews { get; set;}

        public AddEditReviewFormViewModel AddEditReviewFormViewModel { get; set; }
    }


}
