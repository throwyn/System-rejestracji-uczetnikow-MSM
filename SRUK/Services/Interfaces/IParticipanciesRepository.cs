using EntityFrameworkPaginate;
using SRUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services.Interfaces
{
    public interface IParticipanciesRepository
    {
        IEnumerable<ParticipancyShortDTO> GetParticipancies();
        IEnumerable<ParticipancyShortDTO> GetUserParticipancies(string userId);

        ParticipancyDTO GetParticipancy(long id);
        Page<ParticipancyShortDTO> GetFilteredParticipancies(
            short sortBy,
            string firstName,
            string lastName,
            string season,
            bool? conferenceParticipation,
            bool? publication,
            int pageSize,
            int currentPage
        );

        int AddParticipancy(ParticipancyDTO paper);
        int UpdateParticipancy(ParticipancyDTO paper);
        int DeleteParticipancy(long id);
        ParticipancyDTO GetUserCurrentParticipancy(string userId);
        bool UserCanSignToCurrentSeason(string userId);
        bool UserWantsPublicationInThisSeason(string userId);
    }
}
