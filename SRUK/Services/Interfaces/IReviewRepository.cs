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
        Task<int> CreateReviewAsync(ReviewDTO review);
        IEnumerable<ReviewShortDTO> GetUserReviews(string userId);
        Task<int> AddReviewAsync(ReviewDTO review);
    }
}
