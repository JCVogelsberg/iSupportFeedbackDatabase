using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Policy;
using System.Net;
using iSupportFeedback.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PagedList;



namespace iSupportFeedback.Controllers
{
    public partial class MutedFeedbackEntriesController : Controller
    {
        private iSupportFeedbackEntities db = new iSupportFeedbackEntities();

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Entries_Read([DataSourceRequest]DataSourceRequest request, string searchString)
        {
            return Json(GetEntries(searchString).ToDataSourceResult(request));
        }

        private static IEnumerable<MutedFeedbackEntriesViewModel> GetEntries(string searchString)
        {
            var iSupportFeedback = new iSupportFeedbackEntities();

            if (String.IsNullOrEmpty(searchString))
            {
                return iSupportFeedback.MutedFeedbackEntries.Select(mutedFeedbackEntries => new MutedFeedbackEntriesViewModel()

                {
                    Timestamp = mutedFeedbackEntries.Timestamp,
                    Hash = mutedFeedbackEntries.FeedbackEntry.Hash,
                    Version = mutedFeedbackEntries.Version,
                    Message = mutedFeedbackEntries.FeedbackEntry.FeedbackEntryInfoes.FirstOrDefault().Message.Substring(0, 50),
                    SerialNumber = mutedFeedbackEntries.SerialNumber,
                    UnMuted = mutedFeedbackEntries.UnMuted,
                    HideFromView = mutedFeedbackEntries.HideFromView
                });
            }
            else 
            {
                return iSupportFeedback.MutedFeedbackEntries.Where(s => s.FeedbackEntry.Hash.Contains(searchString)
                                                                             || s.SerialNumber.Contains(searchString)
                                                                             || s.Version.Contains(searchString)).Select(mutedFeedbackEntries => new MutedFeedbackEntriesViewModel()

                {
                    Timestamp = mutedFeedbackEntries.Timestamp,
                    Hash = mutedFeedbackEntries.FeedbackEntry.Hash,
                    Version = mutedFeedbackEntries.Version,
                    Message = mutedFeedbackEntries.FeedbackEntry.FeedbackEntryInfoes.FirstOrDefault().Message.Substring(0, 50),
                    SerialNumber = mutedFeedbackEntries.SerialNumber,
                    UnMuted = mutedFeedbackEntries.UnMuted,
                    HideFromView = mutedFeedbackEntries.HideFromView
                });
            }
        }
    }
}


// changed "First()" to "FirstOrDefault()"