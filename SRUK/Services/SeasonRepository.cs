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
            var entitySeason = _context.Season.ToAsyncEnumerable().OrderByDescending(s => s.CreationDate).ToEnumerable();
            var seasons = Mapper.Map<IEnumerable<SeasonShortDTO>>(entitySeason);
            return seasons;
        }

        public int AddSeason(SeasonDTO season)
        {
            Season newSeason = Mapper.Map<Season>(season);
            var seasons = _context.Season.Where(s=>s.IsDeleted != true);
            foreach(var existingSeason in seasons)
            {
                if(!(newSeason.EndDate  <= existingSeason.StartDate || newSeason.StartDate >= existingSeason.EndDate))
                {
                    return -1;
                }
            }

            var status =  _context.Season.Add(newSeason);

            int result =  _context.SaveChanges();
            return result;
        }

        public int UpdateSeason(SeasonDTO season)
        {
            Season seasonNew = Mapper.Map<Season>(season);
            var seasons = _context.Season.Where(s => s.IsDeleted != true && s.Id != season.Id);
            foreach (var existingSeason in seasons)
            {
                if (!(seasonNew.EndDate <= existingSeason.StartDate || seasonNew.StartDate >= existingSeason.EndDate))
                {
                    return -1;
                }
                if (!(seasonNew.ConferenceEndDate <= existingSeason.ConferenceStartDate || seasonNew.ConferenceStartDate >= existingSeason.ConferenceEndDate))
                {
                    return -1;
                }
            }
            var seasonToUpdate =  _context.Season.Find(season.Id);
            
            seasonToUpdate.MainImageFileName = seasonNew.MainImageFileName;
            seasonToUpdate.StartDate = seasonNew.StartDate;
            seasonToUpdate.EndDate = seasonNew.EndDate;
            seasonToUpdate.ConferenceStartDate = seasonNew.ConferenceStartDate;
            seasonToUpdate.ConferenceEndDate = seasonNew.ConferenceEndDate;
            seasonToUpdate.EditionNumber = seasonNew.EditionNumber;
            seasonToUpdate.Location = seasonNew.Location;
            seasonToUpdate.IsDeleted = seasonNew.IsDeleted;
            seasonToUpdate.Name = seasonNew.Name;


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
            Season season = _context.Season.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).SingleOrDefault();
            return (season != null);
        }

        public long GetCurrentSeasonId()
        {
            var season = _context.Season.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).SingleOrDefault();
            return season.Id;
        }

        public SeasonDTO GetCurrentSeason()
        {
            var entitySeason = _context.Season.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).SingleOrDefault();
            if(entitySeason != null)
            {
                var season = Mapper.Map<SeasonDTO>(entitySeason);
                return season;
            }
            return null;
        }
    }
}
