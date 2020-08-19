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
    [Authorize(Roles = "IT,Admin")]
    


    public class ITController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IT
        public ActionResult Index()
        {
            var iT = db.IT.Include(i => i.Employ);
            return View(iT.ToList());
        }

        // GET: IT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IT iT = db.IT.Find(id);
            if (iT == null)
            {
                return HttpNotFound();
            }
            return View(iT);
        }

        // GET: IT/Create
        public ActionResult Create()
        {
            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName");
            return View();
        }

        // POST: IT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Attendance,Departure,EmployID")] IT iT)
        {
            if (ModelState.IsValid)
            {
                db.IT.Add(iT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName", iT.EmployID);
            return View(iT);
        }

        // GET: IT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IT iT = db.IT.Find(id);
            if (iT == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName", iT.EmployID);
            return View(iT);
        }

        // POST: IT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Attendance,Departure,EmployID")] IT iT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployID = new SelectList(db.Employs, "Id", "UserName", iT.EmployID);
            return View(iT);
        }

        // GET: IT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IT iT = db.IT.Find(id);
            if (iT == null)
            {
                return HttpNotFound();
            }
            return View(iT);
        }

        // POST: IT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IT iT = db.IT.Find(id);
            db.IT.Remove(iT);
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
