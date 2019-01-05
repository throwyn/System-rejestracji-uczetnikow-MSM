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
    public class ParticipanciesRepository : IParticipanciesRepository
    {
        private ApplicationDbContext _context;
        public ParticipanciesRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public IEnumerable<ParticipancyShortDTO> GetParticipancies()
        {
            var entityParticipancies = _context.Participancy.Where(p => p.IsDeleted == false).Include(p => p.User).Include(p => p.Season);
            var participancies = Mapper.Map<IEnumerable<ParticipancyShortDTO>>(entityParticipancies);
            return participancies;
        }

        public Page<ParticipancyShortDTO> GetFilteredParticipancies(
            short sortBy,
            string firstName,
            string lastName,
            string season,
            bool? conferenceParticipation,
            bool? publication,
            int pageSize,
            int currentPage
        )
        {
            if (pageSize == 0) pageSize = 10;
            if (currentPage == 0) currentPage = 1;

            IEnumerable<ParticipancyShortDTO> results = GetParticipancies();
            
            results = !string.IsNullOrEmpty(firstName) ? results.Where(u => u.User.FirstName != null && u.User.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(lastName) ? results.Where(u => u.User.LastName != null && u.User.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(season) ? results.Where(u => u.Season.Id == Convert.ToInt32(season)) : results;
            results = conferenceParticipation != null ? results.Where(u => u.ConferenceParticipation == conferenceParticipation) : results;
            results = publication != null ? results.Where(u => u.Publication == publication) : results;

            results = sortBy == 1 ? results.OrderBy(u => u.Season.EndDate) : results;
            results = sortBy == 2 ? results.OrderByDescending(u => u.Season.EndDate) : results;
            results = sortBy == 3 ? results.OrderBy(u => u.User.FirstName) : results;
            results = sortBy == 4 ? results.OrderByDescending(u => u.User.FirstName) : results;
            results = sortBy == 5 ? results.OrderBy(u => u.User.LastName) : results;
            results = sortBy == 6 ? results.OrderByDescending(u => u.User.LastName) : results;
            results = sortBy == 7 ? results.OrderBy(u => u.ConferenceParticipation) : results;
            results = sortBy == 8 ? results.OrderByDescending(u => u.ConferenceParticipation) : results;
            results = sortBy == 9 ? results.OrderBy(u => u.Publication) : results;
            results = sortBy == 10 ? results.OrderByDescending(u => u.Publication) : results;
            results = sortBy == 11 ? results.OrderBy(u => u.CreationDate) : results;
            results = sortBy == 12 ? results.OrderByDescending(u => u.CreationDate) : results;

            int recordCount = results.Count();
            int pageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);
            results = results.Skip((currentPage - 1) * pageSize).Take(pageSize);


            Page<ParticipancyShortDTO> participations = new Page<ParticipancyShortDTO>()
            {
                Results = results,
                RecordCount = recordCount,
                PageCount = pageCount,
                CurrentPage = currentPage,
                PageSize = pageSize

            };

            return participations;
        }

        public IEnumerable<ParticipancyShortDTO> GetUserParticipancies(string userId)
        {
            var entityParticipancies = _context.Participancy.Where(p => p.IsDeleted == false && p.UserId == userId).Include(p => p.User).Include(p => p.Season);
            var participancies = Mapper.Map<IEnumerable<ParticipancyShortDTO>>(entityParticipancies);
            return participancies;
        }

        public ParticipancyDTO GetParticipancy(long id)
        {
            var entityParticipancy = _context.Participancy.SingleOrDefault(p => p.Id == id);
            var participancy = Mapper.Map<ParticipancyDTO>(entityParticipancy);
            return participancy;
        }

        public int AddParticipancy(ParticipancyDTO participancy)
        {
            Participancy newParticipancy = Mapper.Map<Participancy>(participancy);
            _context.Participancy.Add(newParticipancy);

            int result = _context.SaveChanges();
            return result;
        }
        public int UpdateParticipancy(ParticipancyDTO participancy)
        {
            var participancyToUpdate = _context.Participancy.Find(participancy.Id);

            participancyToUpdate.Publication = participancy.Publication;
            participancyToUpdate.ConferenceParticipation = participancy.ConferenceParticipation;
            participancyToUpdate.IsDeleted = participancy.IsDeleted;

            int result = _context.SaveChanges();

            return result;
        }
        public int DeleteParticipancy(long id)
        {
            var participancyToUpdate = _context.Participancy.Find(id);
            participancyToUpdate.IsDeleted = true;
            int result = _context.SaveChanges();
            return result;
        }

        public ParticipancyDTO GetUserCurrentParticipancy(string userId)
        {

            var currentSeason = _context.Season.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).SingleOrDefault();
            if (currentSeason != null) { 
                var entityParticipancy = _context.Participancy.SingleOrDefault(p => p.UserId == userId && p.SeasonId == currentSeason.Id);
                var participancy = Mapper.Map<ParticipancyDTO>(entityParticipancy);
                return participancy;
            }
            else{
                return null;
            }
        }
        public bool UserCanSignToCurrentSeason(string userId)
        {
            bool result;
            var currentSeason = _context.Season.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).SingleOrDefault();
            if (currentSeason != null)
                result = _context.Participancy.Where(p => p.UserId == userId && p.SeasonId == currentSeason.Id).Count() == 0;
            else
                result = false;
            return result;
        }
        public bool UserWantsPublicationInThisSeason(string userId)
        {
            var currentSeason = _context.Season.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).SingleOrDefault();
            if (currentSeason != null) { 
                Participancy participancy = _context.Participancy.Where(p => p.UserId == userId && p.SeasonId == currentSeason.Id).SingleOrDefault();
                if(participancy != null)
                    return participancy.Publication;
            }
            return false;
        }
    }
}
