//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iSupportFeedback.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FeedbackEntry
    {
        public FeedbackEntry()
        {
            this.FeedbackEntryInfoes = new HashSet<FeedbackEntryInfo>();
            this.MutedFeedbackEntries = new HashSet<MutedFeedbackEntry>();
        }
    
        public System.Guid Id { get; set; }
        public string Hash { get; set; }
    
        public virtual ICollection<FeedbackEntryInfo> FeedbackEntryInfoes { get; set; }
        public virtual ICollection<MutedFeedbackEntry> MutedFeedbackEntries { get; set; }
    }
}