using SRUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services.Interfaces
{
    public interface IPaperRepository
    {
        IEnumerable<PaperShortDTO> GetPapers();
        Task<PaperDTO> GetPaperAsync(long id);
        IEnumerable<PaperShortDTO> GetUserPapers(string userId);

        Task<int> AddPaperAsync(PaperDTO paper);
        Task<int> UpdatePaperAsync(PaperDTO paper);
        Task<int> DeletePaperAsync(long id);

        Task<int> SetStatusTopicApproved(long id);
        Task<int> SetStatusTopicRejected(long id);
        Task<int> SetStatuAccepted(long id);
        Task<int> SetStatusDiscarded(long id);
        Task<int> SetStatusSmallMistakesLeft(long id);

        Task<bool> TitleTaken(string title);
        Task<bool> TitleTakenExcept(string title, long id);


        Task<int> UpdatePaperTitleAsync(PaperDTO paper);
    }
}
