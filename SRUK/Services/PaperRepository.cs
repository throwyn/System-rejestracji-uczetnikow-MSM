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
    public class PaperRepository : IPaperRepository
    {
        private ApplicationDbContext _context;
        public PaperRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public int AddPaper(PaperDTO paper)
        {
            Paper newPaper = Mapper.Map<Paper>(paper);
             _context.Paper.Add(newPaper);

            int result =  _context.SaveChanges();
            return result;
        }

        public  PaperDTO GetPaper(long id)
        {
            var entityPaper =  _context.Paper.Include(p => p.Participancy).Include(p => p.Participancy.User).Include(p => p.Participancy.Season).Include(p => p.PaperVersions).Include("PaperVersions.Reviews").SingleOrDefault(p => p.Id == id && p.IsDeleted != true);
            var paper = Mapper.Map<PaperDTO>(entityPaper);
            return paper;
        }

        public Page<PaperShortDTO> GetFilteredPapers(
            short sortBy,
            string season,
            string title,
            string firstName,
            string lastName,
            string status,
            int pageSize,
            int currentPage
        )
        {

            if (pageSize == 0) pageSize = 10;
            if (currentPage == 0) currentPage = 1;

            IEnumerable<PaperShortDTO> results = GetPapers();

            results = !string.IsNullOrEmpty(firstName) ? results.Where(p => p.Participancy.User.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(lastName) ? results.Where(p => p.Participancy.User.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(season) ?   results.Where(p => p.Participancy.Season.Id == Convert.ToInt32(season)) : results;
            results = !string.IsNullOrEmpty(title) ?    results.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(status) ?   results.Where(p => p.Status == Convert.ToInt16(status)) : results;

            results = sortBy == 1 ? results.OrderBy(u => u.Participancy.Season.EndDate) : results;
            results = sortBy == 2 ? results.OrderByDescending(u => u.Participancy.Season.EndDate) : results;
            results = sortBy == 3 ? results.OrderBy(u => u.Title) : results;
            results = sortBy == 4 ? results.OrderByDescending(u => u.Title) : results;
            results = sortBy == 5 ? results.OrderBy(u => u.Participancy.User.FirstName) : results;
            results = sortBy == 6 ? results.OrderByDescending(u => u.Participancy.User.FirstName) : results;
            results = sortBy == 7 ? results.OrderBy(u => u.Participancy.User.LastName) : results;
            results = sortBy == 8 ? results.OrderByDescending(u => u.Participancy.User.LastName) : results;
            results = sortBy == 9 ? results.OrderBy(u => u.CreationDate) : results;
            results = sortBy == 10 || sortBy == 0 ? results.OrderByDescending(u => u.CreationDate) : results;

            int recordCount = results.Count();
            int pageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);
            results = results.Skip((currentPage - 1) * pageSize).Take(pageSize);


            Page<PaperShortDTO> papers = new Page<PaperShortDTO>()
            {
                Results = results,
                RecordCount = recordCount,
                PageCount = pageCount,
                CurrentPage = currentPage,
                PageSize = pageSize

            };

            return papers;
        }

        public bool TitleTaken(string title)
        {
            var paper =  _context.Paper.FirstOrDefault(u => u.Title == title);
            if(paper == null) 
                return false;
            return true;
        }
        public bool TitleTakenExcept(string title, long id)
        {
            var paper =  _context.Paper.FirstOrDefault(p => p.Title == title && p.Id != id);
            if (paper == null)
                return false;
            return true;
        }
        public IEnumerable<PaperShortDTO> GetUserPapers(string userId)
        {
            IEnumerable<Paper> entityPapers = _context.Paper.Include(p => p.PaperVersions).Include(p => p.Participancy.Season).Where(p => p.IsDeleted == false && p.Participancy.User.Id == userId).OrderByDescending(r => r.CreationDate);
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public IEnumerable<PaperShortDTO> GetPapers()
        {
            IEnumerable<Paper> entityPapers = _context.Paper.Include(p => p.Participancy.Season).Include(p => p.Participancy.User).Where(p => p.IsDeleted == false).OrderByDescending(r => r.CreationDate);
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public int UpdatePaper(PaperDTO paper)
        {
            Paper paperNew = Mapper.Map<Paper>(paper);
            var paperToUpdate =  _context.Paper.Find(paper.Id);
            
            paperToUpdate.Title = paperNew.Title;
            paperToUpdate.Status = paperNew.Status;

            int result =  _context.SaveChanges();

            return result;

        }

        public int DeletePaper(long id)
        {

            Paper paper =  _context.Paper.Include(p=>p.PaperVersions).FirstOrDefault(s => s.Id == id);
            if (paper.PaperVersions.Count == 0)
            {
                _context.Paper.Remove(paper);
            }
            else
            {
                paper.Title = paper.Title + " (deleted)";
                paper.IsDeleted = true;
                foreach(var version in paper.PaperVersions)
                {
                    PaperVersion entityVersion = _context.PaperVerison.Include(v=>v.Reviews).FirstOrDefault(v => v.Id == version.Id);
                    entityVersion.IsDeleted = true;
                    if(version.Reviews.Count > 0)
                    {
                        foreach(var review in version.Reviews)
                        {
                            Review entityReview = _context.Review.FirstOrDefault(r => r.Id == review.Id);
                            review.IsDeleted = true;

                        }
                    }
                }
            }

            int result =  _context.SaveChanges();
            return result;
        }

        public int UpdatePaperTitle(PaperDTO paper)
        {
            var paperToUpdate =  _context.Paper.Find(paper.Id);

            paperToUpdate.Title = paper.Title;
            paperToUpdate.Status = 0;

            int result =  _context.SaveChanges();

            return result;
        }

        //Status changers
        public int SetStatusTopicApproved(long id)
        {
            if (!IsCreated(id))
                return 0;
            return  SetStatus(id, 1);
        }

        public int SetStatusTopicRejected(long id)
        {
            if (!IsCreated(id))
                return 0;
            return  SetStatus(id, 2);
        }

        public int SetStatuAccepted(long id)
        {
            return  SetStatus(id, 3);
        }

        public int SetStatusDiscarded(long id)
        {
            return  SetStatus(id, 4);
        }

        public int SetStatusSmallMistakesLeft(long id)
        {
            return  SetStatus(id, 5);
        }

        private bool IsCreated(long id)
        {
            var paper =  _context.Paper.FirstOrDefault(p => p.Id == id);
            if (paper.Status != 0)
            {
                return false;
            }
            return true;
        }

        private int SetStatus(long id,byte status)
        {
            var paper =  _context.Paper.Find(id);
            paper.Status = status;
            int result =  _context.SaveChanges();
            return result;
        }
    }
}
