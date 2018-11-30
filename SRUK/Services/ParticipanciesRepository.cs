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
