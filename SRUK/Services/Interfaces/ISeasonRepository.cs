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
        int AddSeason(SeasonDTO season);
        int UpdateSeason(SeasonDTO season);
        int DeleteSeason(long id);
        long GetCurrentSeasonId();
        bool IsRegistrationOpened();
        SeasonDTO GetCurrentSeason();
    }
}
