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
    public class PaperVersionRepository : IPaperVersionRepository
    {
        private ApplicationDbContext _context;
        public PaperVersionRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public async Task<int> AddPaperVersionAsync(PaperVersionDTO paperVersion)
        {
            PaperVersion newPaper = Mapper.Map<PaperVersion>(paperVersion);
            await _context.PaperVerison.AddAsync(newPaper);

            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<PaperVersionDTO> GetPaperVersionAsync(long id)
        {
            var entityPaperVersion = await _context.PaperVerison.Include(pv => pv.Paper).Include(pv => pv.Reviews).Include(pv => pv.Paper.Author).SingleOrDefaultAsync(u => u.Id == id && u.IsDeleted ==  false);
            var paperVersion = Mapper.Map<PaperVersionDTO>(entityPaperVersion);
            return paperVersion;
        }
        public IEnumerable<PaperVersionShortDTO> GetVersions()
        {
            var entityVersions = _context.PaperVerison.Include(pv => pv.Paper).Include(pv =>pv.Paper.Author).Where(pv => pv.IsDeleted != true);
            var versions = Mapper.Map<IEnumerable<PaperVersionShortDTO>>(entityVersions);
            return versions;
        }

        //public async Task<bool> PaperExists(string title)
        //{
        //    var paper = await _context.Paper.FirstOrDefaultAsync(u => u.Title == title);
        //    if(paper == null) 
        //        return false;
        //    return true;
        //}
        //public IEnumerable<PaperShortDTO> GetUserPapers(string userId)
        //{
        //    var entityPapers = _context.Paper.Where(p => p.IsDeleted == false && p.Author.Id == userId).ToAsyncEnumerable().ToEnumerable();
        //    var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
        //    return papers;
        //}

        //public IEnumerable<PaperShortDTO> GetPapers()
        //{
        //    var entityPapers = _context.Paper.Where(p => p.IsDeleted == false).Include(p => p.Author).ToAsyncEnumerable().ToEnumerable();
        //    var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
        //    return papers;
        //}

        //public async Task<int> UpdatePaperAsync(PaperDTO paper)
        //{
        //    Paper paperNew = Mapper.Map<Paper>(paper);
        //    var paperToUpdate = await _context.Paper.FindAsync(paper.Id);

        //    paperToUpdate.Title = paperNew.Title;
        //    paperToUpdate.Status = paperNew.Status;

        //    int result = await _context.SaveChangesAsync();

        //    return result;

        //}

        /// <summary>
        /// This method delete completely Version!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteVersionAsync(long id)
        {

            PaperVersion paperVersion = await _context.PaperVerison.FirstOrDefaultAsync(s => s.Id == id);
            //paperVersion.IsDeleted = true;
            _context.PaperVerison.Remove(paperVersion);

            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> SetStatusWaitingForReview(long id)
        {
            return await SetStatus(id, 1);
        }

        public async Task<int> SetStatusVersionAccepted(long id)
        {
            return await SetStatus(id, 2);
        }
        public async Task<int> SetStatusVersionRejected(long id)
        {
            return await SetStatus(id, 3);
        }
        public async Task<int> SetStatusWaitingForVerdict(long id)
        {
            return await SetStatus(id, 4);
        }
        public async Task<int> SetStatusSmallMistakes(long id)
        {
            return await SetStatus(id, 5);
        }

        private async Task<int> SetStatus(long id, byte status)
        {
            var version = await _context.PaperVerison.FindAsync(id);
            version.Status = status;
            int result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
