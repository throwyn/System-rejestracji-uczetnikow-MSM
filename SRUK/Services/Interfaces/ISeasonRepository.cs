using SRUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services.Interfaces
{
    public interface ISeasonRepository
    {
        IEnumerable<SeasonShortDTO> GetSeasons();
        SeasonDTO GetSeason(long id);
        Task<int> AddSeasonAsync(SeasonDTO season);
        Task<int> UpdateSeasonAsync(SeasonDTO season);
        Task<int> DeleteSeasonAsync(long id);
        long GetCurrentSeasonId();
        bool IsRegistrationOpened();
        SeasonDTO GetCurrentSeason();
    }
}
