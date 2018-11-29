using SRUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services.Interfaces
{
    public interface IPaperVersionRepository
    {
        int AddPaperVersion(PaperVersionDTO paperVersion);
        PaperVersionDTO GetPaperVersion(long id);
        IEnumerable<PaperVersionShortDTO> GetVersions();
        int DeleteVersion(long id);
        int SetStatusWaitingForReview(long id);
        int SetStatusVersionAccepted(long id);
        int SetStatusVersionRejected(long id);
        int SetStatusWaitingForVerdict(long id);
        int SetStatusSmallMistakes(long id);
        int SetComment(long id, string comment);
    }
}
