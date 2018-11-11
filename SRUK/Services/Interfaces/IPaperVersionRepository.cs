using SRUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services.Interfaces
{
    public interface IPaperVersionRepository
    {
        Task<int> AddPaperVersionAsync(PaperVersionDTO paperVersion);
        Task<PaperVersionDTO> GetPaperVersionAsync(long id);
        IEnumerable<PaperVersionShortDTO> GetVersions();
        Task<int> DeleteVersionAsync(long id);
    }
}
