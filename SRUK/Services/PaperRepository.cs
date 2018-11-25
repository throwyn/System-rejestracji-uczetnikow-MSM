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
    public class PaperRepository : IPaperRepository
    {
        private ApplicationDbContext _context;
        public PaperRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public async Task<int> AddPaperAsync(PaperDTO paper)
        {
            Paper newPaper = Mapper.Map<Paper>(paper);
            await _context.Paper.AddAsync(newPaper);

            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<PaperDTO> GetPaperAsync(long id)
        {
            var entityPaper = await _context.Paper.Include(p => p.Participancy).Include(p => p.Participancy.User).Include(p => p.Participancy.Season).Include(p => p.PaperVersions).Include("PaperVersions.Reviews").SingleOrDefaultAsync(u => u.Id == id);
            var paper = Mapper.Map<PaperDTO>(entityPaper);
            return paper;
        }

        public async Task<bool> TitleTaken(string title)
        {
            var paper = await _context.Paper.FirstOrDefaultAsync(u => u.Title == title);
            if(paper == null) 
                return false;
            return true;
        }
        public async Task<bool> TitleTakenExcept(string title, long id)
        {
            var paper = await _context.Paper.FirstOrDefaultAsync(p => p.Title == title && p.Id != id);
            if (paper == null)
                return false;
            return true;
        }
        public IEnumerable<PaperShortDTO> GetUserPapers(string userId)
        {
            var entityPapers = _context.Paper.Include(p => p.PaperVersions).Include(p => p.Participancy.Season).Where(p => p.IsDeleted == false && p.Participancy.User.Id == userId).ToAsyncEnumerable().ToEnumerable();
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public IEnumerable<PaperShortDTO> GetPapers()
        {
            var entityPapers = _context.Paper.Where(p => p.IsDeleted == false).Include(p => p.Participancy.Season).Include(p => p.Participancy.User).ToAsyncEnumerable().ToEnumerable();
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public async Task<int> UpdatePaperAsync(PaperDTO paper)
        {
            Paper paperNew = Mapper.Map<Paper>(paper);
            var paperToUpdate = await _context.Paper.FindAsync(paper.Id);
            
            paperToUpdate.Title = paperNew.Title;
            paperToUpdate.Status = paperNew.Status;

            int result = await _context.SaveChangesAsync();

            return result;

        }

        public async Task<int> DeletePaperAsync(long id)
        {

            Paper paper = await _context.Paper.Include(p=>p.PaperVersions).FirstOrDefaultAsync(s => s.Id == id);
            if (paper.PaperVersions.Count == 0)
            {
                _context.Paper.Remove(paper);
            }
            else
            {
                paper.Title = paper.Title + " (deleted)";
                paper.IsDeleted = true;
            }

            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdatePaperTitleAsync(PaperDTO paper)
        {
            var paperToUpdate = await _context.Paper.FindAsync(paper.Id);

            paperToUpdate.Title = paper.Title;
            paperToUpdate.Status = 0;

            int result = await _context.SaveChangesAsync();

            return result;
        }

        //Status changers
        public async Task<int> SetStatusTopicApproved(long id)
        {
            if (!IsCreated(id))
                return 0;
            return await SetStatus(id, 1);
        }

        public async Task<int> SetStatusTopicRejected(long id)
        {
            if (!IsCreated(id))
                return 0;
            return await SetStatus(id, 2);
        }

        public async Task<int> SetStatuAccepted(long id)
        {
            return await SetStatus(id, 3);
        }

        public async Task<int> SetStatusDiscarded(long id)
        {
            return await SetStatus(id, 4);
        }

        public async Task<int> SetStatusSmallMistakesLeft(long id)
        {
            return await SetStatus(id, 5);
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

        private async Task<int> SetStatus(long id,byte status)
        {
            var paper = await _context.Paper.FindAsync(id);
            paper.Status = status;
            int result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
