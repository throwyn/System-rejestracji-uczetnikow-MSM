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
        Discarded = 4,
        SmallMistakes = 5
    }

    public enum PaperVersionStatus
    {
        DocumentReceived = 0,
        WaitingForReview = 1,
        Accepted = 2,
        Rejected = 3,
        WaitingForVerdict = 4,
        SmallMistakes = 5
    }
}
