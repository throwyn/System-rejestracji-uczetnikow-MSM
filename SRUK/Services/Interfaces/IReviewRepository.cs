using SRUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<ReviewShortDTO> GetReviews();
        ReviewDTO GetReview(long id);
        int CreateReview(ReviewDTO review);
        IEnumerable<ReviewShortDTO> GetUserReviews(string userId);
        int AddReview(ReviewDTO review);
        IEnumerable<ReviewDTO> GetPaperVersionReviews(long paperVersionId);
        int RemoveReview(long id);
    }
}
