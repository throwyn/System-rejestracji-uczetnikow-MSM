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
    public class PaperVersionRepository : IPaperVersionRepository
    {
        private ApplicationDbContext _context;
        public PaperVersionRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public int AddPaperVersion(PaperVersionDTO paperVersion)
        {
            PaperVersion newPaper = Mapper.Map<PaperVersion>(paperVersion);
             _context.PaperVerison.Add(newPaper);
            _context.SaveChanges();
            return (int)newPaper.Id;
        }
        public Page<PaperVersionShortDTO> GetFilteredVersions(
            short sortBy,
            string title,
            string firstName,
            string lastName,
            string status,
            int pageSize,
            int currentPage)
        {
            if (pageSize == 0) pageSize = 10;
            if (currentPage == 0) currentPage = 1;

            IEnumerable<PaperVersionShortDTO> results = GetVersions();

            results = !string.IsNullOrEmpty(firstName) ? results.Where(p => p.Paper.Participancy.User.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(lastName) ? results.Where(p => p.Paper.Participancy.User.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(title) ? results.Where(p => p.Paper.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(status) ? results.Where(p => p.Status == Convert.ToInt16(status)) : results;
            
            results = sortBy == 1 ? results.OrderBy(u => u.Paper.Title) : results;
            results = sortBy == 2 ? results.OrderByDescending(u => u.Paper.Title) : results;
            results = sortBy == 3 ? results.OrderBy(u => u.Paper.Participancy.User.FirstName) : results;
            results = sortBy == 4 ? results.OrderByDescending(u => u.Paper.Participancy.User.FirstName) : results;
            results = sortBy == 5 ? results.OrderBy(u => u.Paper.Participancy.User.LastName) : results;
            results = sortBy == 6 ? results.OrderByDescending(u => u.Paper.Participancy.User.LastName) : results;
            results = sortBy == 7 ? results.OrderBy(u => u.CreationDate) : results;
            results = sortBy == 8 || sortBy == 0 ? results.OrderByDescending(u => u.CreationDate) : results;

            int recordCount = results.Count();
            int pageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);
            results = results.Skip((currentPage - 1) * pageSize).Take(pageSize);


            Page<PaperVersionShortDTO> papers = new Page<PaperVersionShortDTO>()
            {
                Results = results,
                RecordCount = recordCount,
                PageCount = pageCount,
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            return papers;
        }

        public PaperVersionDTO GetPaperVersion(long id)
        {
            var entityPaperVersion =  _context.PaperVerison.Include(pv => pv.Paper).Include(pv => pv.Reviews).Include(pv => pv.Paper.Participancy).Include(pv => pv.Paper.Participancy.User).SingleOrDefault(u => u.Id == id && u.IsDeleted ==  false);
            var paperVersion = Mapper.Map<PaperVersionDTO>(entityPaperVersion);
            return paperVersion;
        }
        public IEnumerable<PaperVersionShortDTO> GetVersions()
        {
            var entityVersions = _context.PaperVerison.Include(pv => pv.Paper).Include(pv => pv.Reviews).Include(pv => pv.Paper.Participancy).Include(pv => pv.Paper.Participancy.User).Where(pv => pv.IsDeleted != true).OrderByDescending(r => r.CreationDate);
            var versions = Mapper.Map<IEnumerable<PaperVersionShortDTO>>(entityVersions);
            return versions;
        }
        
        /// <summary>
        /// This method delete completely Version!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteVersion(long id)
        {

            PaperVersion paperVersion =  _context.PaperVerison.FirstOrDefault(s => s.Id == id);
            //paperVersion.IsDeleted = true;
            _context.PaperVerison.Remove(paperVersion);

            int result =  _context.SaveChanges();
            return result;
        }

        public int SetStatusDocumentRecieved(long id)
        {
            return SetStatus(id, 0);
        }

        public int SetStatusWaitingForReview(long id)
        {
            return  SetStatus(id, 1);
        }

        public int SetStatusVersionAccepted(long id)
        {
            return  SetStatus(id, 2);
        }
        public int SetStatusVersionRejected(long id)
        {
            return  SetStatus(id, 3);
        }
        public int SetStatusMinorRevision(long id)
        {
            return  SetStatus(id, 4);
        }
        public int SetStatusMajorRevision(long id)
        {
            return  SetStatus(id, 5);
        }

        private int SetStatus(long id, byte status)
        {
            var version =  _context.PaperVerison.Find(id);
            version.Status = status;
            int result =  _context.SaveChanges();
            return result;
        }
        public int SetComment(long id,string comment)
        {
            var paperVerison = _context.PaperVerison.Find(id);
            paperVerison.Comment = comment;
            int result = _context.SaveChanges();
            return result;
        }
    }
}
