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

        public Task<int> AddPaperAsync(PaperDTO season)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePaperAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaperDTO> GetPaperAsync(long id)
        {
            var entityPaper = await _context.Paper.SingleOrDefaultAsync(u => u.Id == id);
            var paper = Mapper.Map<PaperDTO>(entityPaper);
            return paper;
        }

        public IEnumerable<PaperShortDTO> GetPapers()
        {
            var entityPapers = _context.Paper.Where(s => s.IsDeleted == false).ToAsyncEnumerable();
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers.ToEnumerable());
            return papers;
        }

        public Task<int> UpdatePaperAsync(PaperDTO season)
        {
            throw new NotImplementedException();
        }

        //public async Task<int> AddSeasonAsync(SeasonDTO season)
        //{
        //    Season newSeason = Mapper.Map<Season>(season);
        //    var status = await _context.Season.AddAsync(newSeason);

        //    int result = await _context.SaveChangesAsync();
        //    return result;
        //}

        //public async Task<int> UpdateSeasonAsync(SeasonDTO season)
        //{
        //    Season seasonNew = Mapper.Map<Season>(season);
        //    var seasonToUpdate = await _context.Season.FindAsync(season.Id);

        //    seasonToUpdate.LogoFileName = seasonNew.LogoFileName;
        //    seasonToUpdate.MainImageFileName = seasonNew.MainImageFileName;
        //    seasonToUpdate.StartDate = seasonNew.StartDate;
        //    seasonToUpdate.EndDate = seasonNew.EndDate;

        //    int result = await _context.SaveChangesAsync();

        //    return result;

        //}

        //public async Task<int> DeleteSeasonAsync(long id)
        //{
        //    if (_context.Paper.Where(p => p.Season.Id == id).Count() > 0)
        //    {
        //        return 0;
        //    }
        //    Season season = await _context.Season.FirstOrDefaultAsync(s => s.Id == id);
        //    _context.Season.Remove(season);

        //    int result = await _context.SaveChangesAsync();
        //    return result;
        //}
    }
}
