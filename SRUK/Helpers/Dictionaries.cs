using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Helpers
{
    public static class DictionariesHelper
    {
        public static readonly IDictionary<int, string> PaperStatuses = new Dictionary<int, string>()
        {
            {0,"Created" },
            {1,"Topic accepted" },
            {2,"Topic rejected" },
            {3,"Accepted" },
            {4,"Discarded" }
        };
        public static readonly IDictionary<int, string> PaperVersionStatuses = new Dictionary<int, string>()
        {
            {0,"Document recieved" },
            {1,"Waiting for reviews" },
            {2,"Accepted" },
            {3,"Rejected" },
            {4,"Minor revision" },
            {5,"Major revision" }
        };
        public static readonly IDictionary<int, string> RecommendationStatuses = new Dictionary<int, string>()
        {
            {0,"Created" },
            {1,"Cancelled" },
            {2,"Accepted" },
            {3,"Accepted with minor changes" },
            {4,"Major revision" },
            {5,"Rejected" }
        };
        
    }
}
