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
    public class SeasonRepository : ISeasonRepository
    {
        private ApplicationDbContext _context;
        public SeasonRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public SeasonDTO GetSeason(long id)
        {
            var entitySeason = _context.Season.SingleOrDefault(u => u.Id == id);
            var season = Mapper.Map<SeasonDTO>(entitySeason);
            return season;
        }

        public IEnumerable<SeasonShortDTO> GetSeasons()
        {
            var entitySeason = _context.Season.ToAsyncEnumerable().OrderByDescending(s => s.Id).ToEnumerable();
            var seasons = Mapper.Map<IEnumerable<SeasonShortDTO>>(entitySeason);
            return seasons;
        }

        public async Task<int> AddSeasonAsync(SeasonDTO season)
        {
            Season newSeason = Mapper.Map<Season>(season);
            var status = await _context.Season.AddAsync(newSeason);

            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateSeasonAsync(SeasonDTO season)
        {
            Season seasonNew = Mapper.Map<Season>(season);
            var seasonToUpdate = await _context.Season.FindAsync(season.Id);

            seasonToUpdate.LogoFileName = seasonNew.LogoFileName;
            seasonToUpdate.MainImageFileName = seasonNew.MainImageFileName;
            seasonToUpdate.StartDate = seasonNew.StartDate;
            seasonToUpdate.EndDate = seasonNew.EndDate;

            int result =  await _context.SaveChangesAsync();

            return result;
            
        }

        public async Task<int> DeleteSeasonAsync(long id)
        {
            if(_context.Paper.Where(p => p.Season.Id == id).Count() > 0)
            {
                return 0;
            }
            Season season = await _context.Season.FirstOrDefaultAsync(s => s.Id == id);
            _context.Season.Remove(season);

            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<bool> IsRegistrationOpenedAsync()
        {
            Season season = await _context.Season.Where(s => s.StartDate < DateTime.UtcNow && s.EndDate > DateTime.UtcNow).SingleOrDefaultAsync();
            return (season != null);
        }

        public async Task<long> GetCurrentSeasonIdAsync()
        {
            var season = await _context.Season.Where(s => s.StartDate < DateTime.UtcNow && s.EndDate > DateTime.UtcNow).SingleOrDefaultAsync();
            return season.Id;
        }
    }
}
