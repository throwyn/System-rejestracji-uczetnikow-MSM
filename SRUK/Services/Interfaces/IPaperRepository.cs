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
        PaperDTO GetPaper(long id);
        IEnumerable<PaperShortDTO> GetUserPapers(string userId);

        int AddPaper(PaperDTO paper);
        int UpdatePaper(PaperDTO paper);
        int DeletePaper(long id);

        int SetStatusTopicApproved(long id);
        int SetStatusTopicRejected(long id);
        int SetStatuAccepted(long id);
        int SetStatusDiscarded(long id);
        int SetStatusSmallMistakesLeft(long id);

        bool TitleTaken(string title);
        bool TitleTakenExcept(string title, long id);


        int UpdatePaperTitle(PaperDTO paper);
    }
}
