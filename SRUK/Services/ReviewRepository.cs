using AutoMapper;
using EntityFrameworkPaginate;
using Microsoft.EntityFrameworkCore;
using SRUK.Data;
using SRUK.Entities;
using SRUK.Extensions;
using SRUK.Models;
using SRUK.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public IEnumerable<ReviewShortDTO> GetReviews()
        {
            var entityReviews = _context.Review.Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Participancy).Include(r => r.PaperVersion.Paper.Participancy.User).Include(r => r.Critic).Where(r => r.IsDeleted != true);
            var reviews = Mapper.Map<IEnumerable<ReviewShortDTO>>(entityReviews);
            return reviews;
        }

        public Page<ReviewShortDTO> GetFilteredReviews(
            short sortBy,
            string title,
            string firstNameAuthor,
            string lastNameAuthor,
            string firstNameCritic,
            string lastNameCritic,
            string status,
            int pageSize,
            int currentPage
        )
        {

            if (pageSize == 0) pageSize = 10;
            if (currentPage == 0) currentPage = 1;

            IEnumerable<ReviewShortDTO> results = GetReviews();

            results = !string.IsNullOrEmpty(firstNameAuthor) ? results.Where(p => p.PaperVersion.Paper.Participancy.User.FirstName.Contains(firstNameAuthor, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(lastNameAuthor) ? results.Where(p => p.PaperVersion.Paper.Participancy.User.LastName.Contains(lastNameAuthor, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(firstNameCritic) ? results.Where(p => p.Critic.FirstName.Contains(firstNameCritic, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(lastNameCritic) ? results.Where(p => p.Critic.LastName.Contains(lastNameCritic, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(title) ? results.Where(p => p.PaperVersion.Paper.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(status) ? results.Where(p => p.Recommendation == Convert.ToInt16(status)) : results;
            

            results = sortBy == 1 ? results.OrderBy(u => u.PaperVersion.Paper.Title) : results;
            results = sortBy == 2 ? results.OrderByDescending(u => u.PaperVersion.Paper.Title) : results;
            results = sortBy == 3 ? results.OrderBy(u => u.PaperVersion.Paper.Participancy.User.FirstName + u.PaperVersion.Paper.Participancy.User.LastName) : results;
            results = sortBy == 4 ? results.OrderByDescending(u => u.PaperVersion.Paper.Participancy.User.FirstName + u.PaperVersion.Paper.Participancy.User.LastName) : results;
            results = sortBy == 5 ? results.OrderBy(u => u.Critic.FirstName + u.Critic.LastName) : results;
            results = sortBy == 6 ? results.OrderByDescending(u => u.Critic.FirstName + u.Critic.LastName) : results;
            results = sortBy == 7 ? results.OrderBy(u => u.CreationDate) : results;
            results = sortBy == 8  || sortBy == 0? results.OrderByDescending(u => u.CreationDate) : results;

            int recordCount = results.Count();
            int pageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);
            results = results.Skip((currentPage - 1) * pageSize).Take(pageSize);


            Page<ReviewShortDTO> papers = new Page<ReviewShortDTO>()
            {
                Results = results,
                RecordCount = recordCount,
                PageCount = pageCount,
                CurrentPage = currentPage,
                PageSize = pageSize

            };

            return papers;
        }





        public int CreateReview(ReviewDTO review)
        {
            var entityReview = Mapper.Map<Review>(review);
            _context.Review.Add(entityReview);
            var result = _context.SaveChanges();
            return result;

        }
        public int CreateReviews(IEnumerable<ReviewDTO> reviews)
        {
            var entityReviews = Mapper.Map<IEnumerable<Review>>(reviews);
            _context.Review.AddRange(entityReviews);
            var result = _context.SaveChanges();
            return result;

        }
        public IEnumerable<ReviewShortDTO> GetUserReviews(string userId)
        {
            var entityReviews = _context.Review.Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Participancy).Include(r => r.PaperVersion.Paper.Participancy.User).Where(r => r.CriticId == userId);
            var reviews = Mapper.Map<IEnumerable<ReviewShortDTO>>(entityReviews);

            return reviews;
        }
        public ReviewDTO GetReview(long id)
        {
            var entityReview = _context.Review.Include(r => r.Critic).Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Participancy).Include(r => r.PaperVersion.Paper.Participancy.User).SingleOrDefaultAsync(r => r.Id == id).Result;
            var review = Mapper.Map<ReviewDTO>(entityReview);
            return review;
        }
        public IEnumerable<ReviewDTO> GetPaperVersionReviews(long paperVersionId)
        {
            var entityReview = _context.Review.Where(r => r.PaperVersionId == paperVersionId);
            var reviews = Mapper.Map<IEnumerable<ReviewDTO>>(entityReview);
            return reviews;
        }
        public int AddReview(ReviewDTO review)
        {
            var entityReview =  _context.Review.Find(review.Id);

            entityReview.FileName = review.FileName;
            entityReview.OriginalFileName = review.OriginalFileName;

            entityReview.Recommendation = review.Recommendation;
            entityReview.CompletionDate = review.CompletionDate;

            entityReview.CommentForAdmin = review.CommentForAdmin;
            entityReview.CommentForAuthor = review.CommentForAuthor;
            var result = _context.SaveChanges();
            return result;
        }
        public int RemoveReview(long id)
        {
            Review review =  _context.Review.FirstOrDefault(s => s.Id == id);
            _context.Review.Remove(review);

            int result = _context.SaveChanges();
            return result;
        }
        public int SetRecommendationCancelled(long id)
        {
            Review review = _context.Review.FirstOrDefault(s => s.Id == id);
            review.Recommendation = 1;
            int result = _context.SaveChanges();
            return result;
        }


    }
}
