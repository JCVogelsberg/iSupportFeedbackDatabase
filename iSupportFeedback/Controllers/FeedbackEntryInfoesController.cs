using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iSupportFeedback.Models;
using PagedList;

namespace iSupportFeedback.Controllers
{
    public class FeedbackEntryInfoesController : Controller
    {
        private iSupportFeedbackEntities db = new iSupportFeedbackEntities();

        // GET: FeedbackEntryInfoes ------------------------------------------------------------------------  Index
        public ActionResult Index(string sortOrder, string searchThing, int? page)
    {
        ViewBag.TimestampSortParm = sortOrder == "Timestamp" ? "timestamp_desc" : "Timestamp";
        ViewBag.HashSortParm = sortOrder == "Hash" ? "hash_desc" : "Hash";
        ViewBag.LogEntryLevelSortParm = sortOrder == "LogEntrylevel" ? "logentrylevel_desc" : "LogEntrylevel";
        ViewBag.VersionSortParm = sortOrder == "Version" ? "version_desc" : "Version";
        ViewBag.SerialNumberSortParm = sortOrder == "SerialNumber" ? "serialnumber_desc" : "SerialNumber";
        ViewBag.OccuranceCountSortParm = sortOrder == "OccuranceCount" ? "occurancecount_desc" : "OccuranceCount";

        var FeedbackEntryInfoes = from s in db.FeedbackEntryInfoes
                  select s;


        if (!String.IsNullOrEmpty(searchThing))
    	{
		    FeedbackEntryInfoes = FeedbackEntryInfoes.Where(s => s.FeedbackEntry.Hash.Contains(searchThing)
                                                                    || s.SerialNumber.Contains(searchThing));
        }


        switch (sortOrder)
        {
        case "timestamp_desc":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderByDescending(s => s.Timestamp);
            break;
	     case "Hash":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderBy(s => s.FeedbackEntry.Hash);
            break;
         case "hash_desc":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderByDescending(s => s.FeedbackEntry.Hash);
            break;
	     case "LogEntryLevel":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderBy(s => s.LogEntryLevel);
            break;
         case "logentrylevel_desc":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderByDescending(s => s.LogEntryLevel);
            break;
	     case "Version":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderBy(s => s.Version);
            break;
         case "version_desc":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderByDescending(s => s.Version);
            break;
	     case "SerialNumber":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderBy(s => s.SerialNumber);
            break;
         case "serialnumber_desc":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderByDescending(s => s.SerialNumber);
            break;
         case "OccuranceCount":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderBy(s => s.OccuranceCount);
            break;
         case "occurancecount_desc":
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderByDescending(s => s.OccuranceCount);
            break;
         default:             
            FeedbackEntryInfoes = FeedbackEntryInfoes.OrderBy(s => s.Timestamp);
            break;
    }

    return View(FeedbackEntryInfoes.ToList());
}


        // GET: FeedbackEntryInfoes/Details/5     ------------------------------------------------------------------------  Details
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackEntryInfo feedbackEntryInfo = db.FeedbackEntryInfoes.Find(id);
            if (feedbackEntryInfo == null)
            {
                return HttpNotFound();
            }
            return View(feedbackEntryInfo);
        }

        // GET: FeedbackEntryInfoes/Create     ------------------------------------------------------------------------  Create
        public ActionResult Create()
        {
            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash");
            return View();
        }

        // POST: FeedbackEntryInfoes/Create      ------------------------------------------------------------------------  Create, Antiforgery
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Source,LogEntryLevel,EventId,Message,Browser,IsMobile,Version,SerialNumber,OccuranceCount,EntryId,Timestamp")] FeedbackEntryInfo feedbackEntryInfo)
        {
            if (ModelState.IsValid)
            {
                feedbackEntryInfo.Id = Guid.NewGuid();
                db.FeedbackEntryInfoes.Add(feedbackEntryInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash", feedbackEntryInfo.EntryId);
            return View(feedbackEntryInfo);
        }

        // GET: FeedbackEntryInfoes/Edit/5     ------------------------------------------------------------------------  Edit
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackEntryInfo feedbackEntryInfo = db.FeedbackEntryInfoes.Find(id);
            if (feedbackEntryInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash", feedbackEntryInfo.EntryId);
            return View(feedbackEntryInfo);
        }

        // POST: FeedbackEntryInfoes/Edit/5     ------------------------------------------------------------------------  Edit, Antiforgery
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Source,LogEntryLevel,EventId,Message,Browser,IsMobile,Version,SerialNumber,OccuranceCount,EntryId,Timestamp")] FeedbackEntryInfo feedbackEntryInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackEntryInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash", feedbackEntryInfo.EntryId);
            return View(feedbackEntryInfo);
        }

        // GET: FeedbackEntryInfoes/Delete/5     ------------------------------------------------------------------------  Delete
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackEntryInfo feedbackEntryInfo = db.FeedbackEntryInfoes.Find(id);
            if (feedbackEntryInfo == null)
            {
                return HttpNotFound();
            }
            return View(feedbackEntryInfo);
        }

        // POST: FeedbackEntryInfoes/Delete/5     ------------------------------------------------------------------------  Delete, Antiforgery
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FeedbackEntryInfo feedbackEntryInfo = db.FeedbackEntryInfoes.Find(id);
            db.FeedbackEntryInfoes.Remove(feedbackEntryInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
