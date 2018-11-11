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
            var entityPaper = await _context.Paper.Include(p => p.Author).Include(p => p.Season).Include(p => p.PaperVersions).SingleOrDefaultAsync(u => u.Id == id);
            var paper = Mapper.Map<PaperDTO>(entityPaper);
            return paper;
        }

        public async Task<bool> PaperExists(string title)
        {
            var paper = await _context.Paper.FirstOrDefaultAsync(u => u.Title == title);
            if(paper == null) 
                return false;
            return true;
        }
        public IEnumerable<PaperShortDTO> GetUserPapers(string userId)
        {
            var entityPapers = _context.Paper.Where(p => p.IsDeleted == false && p.Author.Id == userId).ToAsyncEnumerable().ToEnumerable();
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public IEnumerable<PaperShortDTO> GetPapers()
        {
            var entityPapers = _context.Paper.Where(p => p.IsDeleted == false).Include(p => p.Author).ToAsyncEnumerable().ToEnumerable();
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
            
            Paper paper = await _context.Paper.FirstOrDefaultAsync(s => s.Id == id);
            paper.IsDeleted = true;

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
        public async Task<int> ApproveTopic(long id)
        {
            int result;
            var paper = await _context.Paper.FindAsync(id);
            if (paper.Status == 0)
            {
                paper.Status = 1;
                result = await _context.SaveChangesAsync();
            }
            else
            {
                result = 2;
            }

            return result;

        }
        public async Task<int> RejectTopic(long id)
        {
            int result;
            var paper = await _context.Paper.FindAsync(id);
            if (paper.Status == 0)
            {
                paper.Status = 2;
                result = await _context.SaveChangesAsync();
            }
            else
            {
                result = 2;
            }

            return result;

        }
    }
}
