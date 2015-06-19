using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iSupportFeedback.Models
{
    public class MutedFeedbackEntriesViewModel
    {
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; }
        public string Version { get; set; }
        public string Message { get; set; }
        public string SerialNumber { get; set; }
        public bool UnMuted { get; set; }
        public bool HideFromView { get; set; }
    }
}