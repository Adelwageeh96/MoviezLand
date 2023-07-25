using MoviezLand.Core.Models;

namespace MoviezLand.Web.ViewModels.Shared
{
    public class MovieWithReviewsViewModel
    {
        public Movie movie { get; set; }

        public IEnumerable<Review> reviews { get; set;}
    }

}
