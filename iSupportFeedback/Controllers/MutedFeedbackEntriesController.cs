using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using iSupportFeedback.Models;
using PagedList;

namespace iSupportFeedback.Controllers
{
    public class MutedFeedbackEntriesController : Controller
    {
        private iSupportFeedbackEntities db = new iSupportFeedbackEntities();

        // GET: MutedFeedbackEntries    <!-- -------------------------------------------------------------------------------------- Index_Method -->
        public ActionResult Index(string sortOrder, string searchString)
        {

            ViewBag.TimestampSortParm = sortOrder == "Timestamp" ? "timestamp_desc" : "Timestamp";
            ViewBag.HashSortParm = sortOrder == "Hash" ? "hash_desc" : "Hash";
            ViewBag.SerialNumberSortParm = sortOrder == "SerialNumber" ? "serialnumber_desc" : "SerialNumber";

            var mutedFeedbackEntries = from s in db.MutedFeedbackEntries
                                       select s;

            if (!String.IsNullOrEmpty(searchString))
		    {
                mutedFeedbackEntries = mutedFeedbackEntries.Where(s => s.FeedbackEntry.Hash.Contains(searchString)
                                                                         || s.SerialNumber.Contains(searchString));
		    }
            
    
           switch (sortOrder)
           {
                case "timestamp_desc":
                    mutedFeedbackEntries = mutedFeedbackEntries.OrderByDescending(s => s.Timestamp);
                    break;
	            case "Hash":
                    mutedFeedbackEntries = mutedFeedbackEntries.OrderBy(s => s.FeedbackEntry.Hash);
                    break;
                case "hash_desc":
                    mutedFeedbackEntries = mutedFeedbackEntries.OrderByDescending(s => s.FeedbackEntry.Hash);
                    break;
	            case "SerialNumber":
                    mutedFeedbackEntries = mutedFeedbackEntries.OrderBy(s => s.SerialNumber);
                    break;
                case "serialnumber_desc":
                    mutedFeedbackEntries = mutedFeedbackEntries.OrderByDescending(s => s.SerialNumber);
                    break;
                default:             
                    mutedFeedbackEntries = mutedFeedbackEntries.OrderBy(s => s.Timestamp);
	            break;
            }

           return View(mutedFeedbackEntries.ToList());
        }



        // GET: MutedFeedbackEntries/Details/5    <!-- --------------------------------------------------------------------------- Details_Method -->
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MutedFeedbackEntry mutedFeedbackEntry = db.MutedFeedbackEntries.Find(id);
            if (mutedFeedbackEntry == null)
            {
                return HttpNotFound();
            }
            return View(mutedFeedbackEntry);
        }

        // GET: MutedFeedbackEntries/Create    <!-- --------------------------------------------------------------------------------- Create_Method -->
        public ActionResult Create()
        {
            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash");
            return View();
        }

        // POST: MutedFeedbackEntries/Create    <!-- --------------------------------------------------------------------- Create_Method_AntiForgery -->
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Timestamp,Version,SerialNumber,EntryId,UnMuted,HideFromView")] MutedFeedbackEntry mutedFeedbackEntry)
        {
            if (ModelState.IsValid)
            {
                mutedFeedbackEntry.Id = Guid.NewGuid();
                db.MutedFeedbackEntries.Add(mutedFeedbackEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash", mutedFeedbackEntry.EntryId);
            return View(mutedFeedbackEntry);
        }

        // GET: MutedFeedbackEntries/Edit/5    <!-- ------------------------------------------------------------------------------------- Edit_Method -->
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MutedFeedbackEntry mutedFeedbackEntry = db.MutedFeedbackEntries.Find(id);
            if (mutedFeedbackEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash", mutedFeedbackEntry.EntryId);
            return View(mutedFeedbackEntry);
        }

        // POST: MutedFeedbackEntries/Edit/5    <!-- ------------------------------------------------------------------------- Edit_Method_AntiForgery -->
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Timestamp,Version,SerialNumber,EntryId,UnMuted,HideFromView")] MutedFeedbackEntry mutedFeedbackEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mutedFeedbackEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntryId = new SelectList(db.FeedbackEntries, "Id", "Hash", mutedFeedbackEntry.EntryId);
            return View(mutedFeedbackEntry);
        }

        // GET: MutedFeedbackEntries/Delete/5    <!-- ---------------------------------------------------------------------------------- Delete_Method -->
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MutedFeedbackEntry mutedFeedbackEntry = db.MutedFeedbackEntries.Find(id);
            if (mutedFeedbackEntry == null)
            {
                return HttpNotFound();
            }
            return View(mutedFeedbackEntry);
        }

        // POST: MutedFeedbackEntries/Delete/5    <!-- ---------------------------------------------------------------------- Delete_Method_AntiForgery -->
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MutedFeedbackEntry mutedFeedbackEntry = db.MutedFeedbackEntries.Find(id);
            db.MutedFeedbackEntries.Remove(mutedFeedbackEntry);
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