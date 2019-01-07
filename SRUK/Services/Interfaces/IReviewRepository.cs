using EntityFrameworkPaginate;
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
        Page<ReviewShortDTO> GetFilteredReviews(short sortBy,string title,string firstNameAuthor,string lastNameAuthor,string firstNameCritic,string lastNameCritic,string status,int pageSize,int currentPage);
        ReviewDTO GetReview(long id);
        int CreateReview(ReviewDTO review);
        int CreateReviews(IEnumerable<ReviewDTO> reviews);
        IEnumerable<ReviewShortDTO> GetUserReviews(string userId);
        int AddReview(ReviewDTO review);
        IEnumerable<ReviewDTO> GetPaperVersionReviews(long paperVersionId);
        int RemoveReview(long id);
        int SetRecommendationCancelled(long id);
    }
}
