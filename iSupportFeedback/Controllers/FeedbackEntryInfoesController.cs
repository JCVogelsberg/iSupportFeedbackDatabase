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
    public partial class FeedbackEntryInfoesController : Controller
    {


        protected override void Dispose(bool disposing)
        {
            //productService.Dispose();

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

        private static IEnumerable<FeedbackEntryInfoesViewModel> GetEntries(string searchString)
        {
            var iSupportFeedback = new iSupportFeedbackEntities();

            if (String.IsNullOrEmpty(searchString))
            {
                return iSupportFeedback.FeedbackEntryInfoes.Select(feedbackEntryInfoes => new FeedbackEntryInfoesViewModel()

                {
                    Timestamp = feedbackEntryInfoes.Timestamp,
                    Hash = feedbackEntryInfoes.FeedbackEntry.Hash,
                    LogEntryLevel = feedbackEntryInfoes.LogEntryLevel,
                    Version = feedbackEntryInfoes.Version,
                    SerialNumber = feedbackEntryInfoes.SerialNumber,
                    OccuranceCount = feedbackEntryInfoes.OccuranceCount,
                    Message = feedbackEntryInfoes.Message.Substring(0, 50)
                });
            }
            else
            {
                return iSupportFeedback.FeedbackEntryInfoes.Where(s => s.FeedbackEntry.Hash.Contains(searchString)
                                                                             || s.SerialNumber.Contains(searchString)
                                                                             || s.Version.Contains(searchString)).Select(feedbackEntryInfoes => new FeedbackEntryInfoesViewModel()

                {
                    Timestamp = feedbackEntryInfoes.Timestamp,
                    Hash = feedbackEntryInfoes.FeedbackEntry.Hash,
                    LogEntryLevel = feedbackEntryInfoes.LogEntryLevel,
                    Version = feedbackEntryInfoes.Version,
                    SerialNumber = feedbackEntryInfoes.SerialNumber,
                    OccuranceCount = feedbackEntryInfoes.OccuranceCount,
                    Message = feedbackEntryInfoes.Message.Substring(0, 50)
                });
            }
        }
    }
}