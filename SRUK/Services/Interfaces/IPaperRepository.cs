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
        Task<int> ApproveTopic(long id);
        Task<int> RejectTopic(long id);

        Task<bool> PaperExists(string title);


        Task<int> UpdatePaperTitleAsync(PaperDTO paper);
    }
}
