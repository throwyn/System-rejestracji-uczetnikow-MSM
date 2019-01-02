using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public enum PaperStatus
    {
        Created = 0,
        TopicAccepted = 1,
        TopicRejected = 2,
        Accepted = 3,
        Discarded = 4
    }

    public enum PaperVersionStatus
    {
        DocumentReceived = 0,
        WaitingForReviews = 1,
        Accepted = 2,
        Rejected = 3,
        MinorRevision = 4,
        MajorRevision = 5
    }
    public enum ReviewRecommendationStatus
    {
        Created = 0,
        Canceled = 1,
        Accepted = 2,
        AcceptedWithMinorChanges = 3,
        MajorRevision = 4,
        Rejected = 5
    }
}
