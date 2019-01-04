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
    public class PaperVersionRepository : IPaperVersionRepository
    {
        private ApplicationDbContext _context;
        public PaperVersionRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public int AddPaperVersion(PaperVersionDTO paperVersion)
        {
            PaperVersion newPaper = Mapper.Map<PaperVersion>(paperVersion);
             _context.PaperVerison.Add(newPaper);
            _context.SaveChanges();
            return (int)newPaper.Id;
        }

        public PaperVersionDTO GetPaperVersion(long id)
        {
            var entityPaperVersion =  _context.PaperVerison.Include(pv => pv.Paper).Include(pv => pv.Reviews).Include(pv => pv.Paper.Participancy).Include(pv => pv.Paper.Participancy.User).SingleOrDefault(u => u.Id == id && u.IsDeleted ==  false);
            var paperVersion = Mapper.Map<PaperVersionDTO>(entityPaperVersion);
            return paperVersion;
        }
        public IEnumerable<PaperVersionShortDTO> GetVersions()
        {
            var entityVersions = _context.PaperVerison.Include(pv => pv.Paper).Include(pv => pv.Reviews).Include(pv => pv.Paper.Participancy).Include(pv => pv.Paper.Participancy.User).Where(pv => pv.IsDeleted != true);
            var versions = Mapper.Map<IEnumerable<PaperVersionShortDTO>>(entityVersions);
            return versions;
        }
        
        /// <summary>
        /// This method delete completely Version!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteVersion(long id)
        {

            PaperVersion paperVersion =  _context.PaperVerison.FirstOrDefault(s => s.Id == id);
            //paperVersion.IsDeleted = true;
            _context.PaperVerison.Remove(paperVersion);

            int result =  _context.SaveChanges();
            return result;
        }

        public int SetStatusDocumentRecieved(long id)
        {
            return SetStatus(id, 0);
        }

        public int SetStatusWaitingForReview(long id)
        {
            return  SetStatus(id, 1);
        }

        public int SetStatusVersionAccepted(long id)
        {
            return  SetStatus(id, 2);
        }
        public int SetStatusVersionRejected(long id)
        {
            return  SetStatus(id, 3);
        }
        public int SetStatusMinorRevision(long id)
        {
            return  SetStatus(id, 4);
        }
        public int SetStatusMajorRevision(long id)
        {
            return  SetStatus(id, 5);
        }

        private int SetStatus(long id, byte status)
        {
            var version =  _context.PaperVerison.Find(id);
            version.Status = status;
            int result =  _context.SaveChanges();
            return result;
        }
        public int SetComment(long id,string comment)
        {
            var paperVerison = _context.PaperVerison.Find(id);
            paperVerison.Comment = comment;
            int result = _context.SaveChanges();
            return result;
        }
    }
}
