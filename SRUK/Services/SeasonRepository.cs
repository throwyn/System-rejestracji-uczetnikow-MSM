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

        public int AddSeason(SeasonDTO season)
        {
            Season newSeason = Mapper.Map<Season>(season);
            var status =  _context.Season.Add(newSeason);

            int result =  _context.SaveChanges();
            return result;
        }

        public int UpdateSeason(SeasonDTO season)
        {
            Season seasonNew = Mapper.Map<Season>(season);
            var seasonToUpdate =  _context.Season.Find(season.Id);

            seasonToUpdate.LogoFileName = seasonNew.LogoFileName;
            seasonToUpdate.MainImageFileName = seasonNew.MainImageFileName;
            seasonToUpdate.StartDate = seasonNew.StartDate;
            seasonToUpdate.EndDate = seasonNew.EndDate;

            int result =   _context.SaveChanges();

            return result;
            
        }

        public int DeleteSeason(long id)
        {
            if(_context.Paper.Where(p => p.Participancy.Season.Id == id).Count() > 0)
            {
                return 0;
            }
            Season season =  _context.Season.FirstOrDefault(s => s.Id == id);
            _context.Season.Remove(season);

            int result =  _context.SaveChanges();
            return result;
        }
        public bool IsRegistrationOpened()
        {
            Season season = _context.Season.Where(s => s.StartDate < DateTime.UtcNow && s.EndDate > DateTime.UtcNow).SingleOrDefault();
            return (season != null);
        }

        public long GetCurrentSeasonId()
        {
            var season = _context.Season.Where(s => s.StartDate < DateTime.UtcNow && s.EndDate > DateTime.UtcNow).SingleOrDefault();
            return season.Id;
        }

        public SeasonDTO GetCurrentSeason()
        {
            var entitySeason = _context.Season.Where(s => s.StartDate < DateTime.UtcNow && s.EndDate > DateTime.UtcNow).SingleOrDefault();

            var season = Mapper.Map<SeasonDTO>(entitySeason);
            return season;
        }
    }
}
