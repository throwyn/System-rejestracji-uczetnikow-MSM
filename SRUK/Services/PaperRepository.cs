using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SRUK.Data;
using SRUK.Entities;
using SRUK.Models;
using SRUK.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Services
{
    public class PaperRepository : IPaperRepository
    {
        private ApplicationDbContext _context;
        public PaperRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public int AddPaper(PaperDTO paper)
        {
            Paper newPaper = Mapper.Map<Paper>(paper);
             _context.Paper.Add(newPaper);

            int result =  _context.SaveChanges();
            return result;
        }

        public  PaperDTO GetPaper(long id)
        {
            var entityPaper =  _context.Paper.Include(p => p.Participancy).Include(p => p.Participancy.User).Include(p => p.Participancy.Season).Include(p => p.PaperVersions).Include("PaperVersions.Reviews").SingleOrDefault(p => p.Id == id && p.IsDeleted != true);
            var paper = Mapper.Map<PaperDTO>(entityPaper);
            return paper;
        }

        public bool TitleTaken(string title)
        {
            var paper =  _context.Paper.FirstOrDefault(u => u.Title == title);
            if(paper == null) 
                return false;
            return true;
        }
        public bool TitleTakenExcept(string title, long id)
        {
            var paper =  _context.Paper.FirstOrDefault(p => p.Title == title && p.Id != id);
            if (paper == null)
                return false;
            return true;
        }
        public IEnumerable<PaperShortDTO> GetUserPapers(string userId)
        {
            var entityPapers = _context.Paper.Include(p => p.PaperVersions).Include(p => p.Participancy.Season).Where(p => p.IsDeleted == false && p.Participancy.User.Id == userId).ToAsyncEnumerable().ToEnumerable();
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public IEnumerable<PaperShortDTO> GetPapers()
        {
            var entityPapers = _context.Paper.Include(p => p.Participancy.Season).Include(p => p.Participancy.User).Where(p => p.IsDeleted == false).ToAsyncEnumerable().ToEnumerable();
            var papers = Mapper.Map<IEnumerable<PaperShortDTO>>(entityPapers);
            return papers;
        }

        public int UpdatePaper(PaperDTO paper)
        {
            Paper paperNew = Mapper.Map<Paper>(paper);
            var paperToUpdate =  _context.Paper.Find(paper.Id);
            
            paperToUpdate.Title = paperNew.Title;
            paperToUpdate.Status = paperNew.Status;

            int result =  _context.SaveChanges();

            return result;

        }

        public int DeletePaper(long id)
        {

            Paper paper =  _context.Paper.Include(p=>p.PaperVersions).FirstOrDefault(s => s.Id == id);
            if (paper.PaperVersions.Count == 0)
            {
                _context.Paper.Remove(paper);
            }
            else
            {
                paper.Title = paper.Title + " (deleted)";
                paper.IsDeleted = true;
            }

            int result =  _context.SaveChanges();
            return result;
        }

        public int UpdatePaperTitle(PaperDTO paper)
        {
            var paperToUpdate =  _context.Paper.Find(paper.Id);

            paperToUpdate.Title = paper.Title;
            paperToUpdate.Status = 0;

            int result =  _context.SaveChanges();

            return result;
        }

        //Status changers
        public int SetStatusTopicApproved(long id)
        {
            if (!IsCreated(id))
                return 0;
            return  SetStatus(id, 1);
        }

        public int SetStatusTopicRejected(long id)
        {
            if (!IsCreated(id))
                return 0;
            return  SetStatus(id, 2);
        }

        public int SetStatuAccepted(long id)
        {
            return  SetStatus(id, 3);
        }

        public int SetStatusDiscarded(long id)
        {
            return  SetStatus(id, 4);
        }

        public int SetStatusSmallMistakesLeft(long id)
        {
            return  SetStatus(id, 5);
        }

        private bool IsCreated(long id)
        {
            var paper =  _context.Paper.FirstOrDefault(p => p.Id == id);
            if (paper.Status != 0)
            {
                return false;
            }
            return true;
        }

        private int SetStatus(long id,byte status)
        {
            var paper =  _context.Paper.Find(id);
            paper.Status = status;
            int result =  _context.SaveChanges();
            return result;
        }
    }
}
