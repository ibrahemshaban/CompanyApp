using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Companyy.Models;

namespace Companyy.Controllers
{
    [Authorize(Roles = "HR,Admin")]

    public class HRsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HRs
        public ActionResult Index()
        {
            var hRs = db.HRs.Include(h => h.Employ);
            return View(hRs.ToList());
        }

        // GET: HRs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HR hR = db.HRs.Find(id);
            if (hR == null)
            {
                return HttpNotFound();
            }
            return View(hR);
        }

        // GET: HRs/Create
        public ActionResult Create()
        {
            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName");
            return View();
        }

        // POST: HRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployID,Department,Hours,Days")] HR hR)
        {
            if (ModelState.IsValid)
            {
                db.HRs.Add(hR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName", hR.EmployID);
            return View(hR);
        }

        // GET: HRs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HR hR = db.HRs.Find(id);
            if (hR == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName", hR.EmployID);
            return View(hR);
        }

        // POST: HRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployID,Department,Hours,Days")] HR hR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName", hR.EmployID);
            return View(hR);
        }

        // GET: HRs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HR hR = db.HRs.Find(id);
            if (hR == null)
            {
                return HttpNotFound();
            }
            return View(hR);
        }

        // POST: HRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HR hR = db.HRs.Find(id);
            db.HRs.Remove(hR);
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
