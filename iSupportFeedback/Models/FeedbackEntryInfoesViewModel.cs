using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iSupportFeedback.Models
{
    public class FeedbackEntryInfoesViewModel
    {
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; }
        public int LogEntryLevel { get; set; }
        public string Version { get; set; }
        public string SerialNumber { get; set; }
        public int OccuranceCount { get; set; }
        public string Message { get; set; }
    }
}