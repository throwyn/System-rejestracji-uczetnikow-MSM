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
            var entityReviews = _context.Review.Include(r => r.PaperVersion).Include(r => r.PaperVersion.Paper).Include(r => r.PaperVersion.Paper.Participancy).Include(r => r.PaperVersion.Paper.Participancy.User).Include(r => r.Critic).Where(r => r.IsDeleted != true);
            var reviews = Mapper.Map<IEnumerable<ReviewShortDTO>>(entityReviews);
            return reviews;
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


    }
}
