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
        Task<int> AddPaperAsync(PaperDTO season);
        Task<int> UpdatePaperAsync(PaperDTO season);
        Task<int> DeletePaperAsync(long id);
    }
}
