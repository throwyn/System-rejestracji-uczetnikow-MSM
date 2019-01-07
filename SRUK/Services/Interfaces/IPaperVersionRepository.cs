using EntityFrameworkPaginate;
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
        Page<PaperVersionShortDTO> GetFilteredVersions(short sortBy,string title,string firstName,string lastName,string status,int pageSize,int currentPage);
        IEnumerable<PaperVersionShortDTO> GetVersions();
        int DeleteVersion(long id);
        int SetStatusDocumentRecieved(long id);
        int SetStatusWaitingForReview(long id);
        int SetStatusVersionAccepted(long id);
        int SetStatusVersionRejected(long id);
        int SetStatusMinorRevision(long id);
        int SetStatusMajorRevision(long id);
        int SetComment(long id, string comment);

    }
}
