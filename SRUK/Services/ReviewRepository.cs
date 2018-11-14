using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SRUK.Data;
using SRUK.Entities;
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
            var entityReviews = _context.Review.Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Author).Include(r => r.Critic).Where(r => r.IsDeleted != true);
            var reviews = Mapper.Map<IEnumerable<ReviewShortDTO>>(entityReviews);
            return reviews;
        }
        public async Task<int> CreateReviewAsync(ReviewDTO review)
        {
            var entityReview = Mapper.Map<Review>(review);
            await _context.Review.AddAsync(entityReview);
            var result = _context.SaveChangesAsync().Result;
            return result;

        }
        public IEnumerable<ReviewShortDTO> GetUserReviews(string userId)
        {
            var entityReviews = _context.Review.Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Author).Where(r => r.CriticId == userId);
            var reviews = Mapper.Map<IEnumerable<ReviewShortDTO>>(entityReviews);

            return reviews;
        }
        public ReviewDTO GetReview(long id)
        {
            var entityReview = _context.Review.Include(r => r.Critic).Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Author).SingleOrDefaultAsync(r => r.Id == id).Result;
            var review = Mapper.Map<ReviewDTO>(entityReview);
            return review;
        }
        public async Task<int> AddReviewAsync(ReviewDTO review)
        {
            var entityReview = await _context.Review.FindAsync(review.Id);

            entityReview.FileName = review.FileName;
            entityReview.OriginalFileName = review.OriginalFileName;

            entityReview.EditorialErrors = review.EditorialErrors;
            entityReview.TechnicalErrors = review.TechnicalErrors;
            entityReview.RepeatReview = review.RepeatReview;
            entityReview.IsPulp = review.IsPulp;
            entityReview.IsPositive = review.IsPositive;

            entityReview.Comment = review.Comment;
            var result =  await _context.SaveChangesAsync();
            return result;
        }


    }
}
