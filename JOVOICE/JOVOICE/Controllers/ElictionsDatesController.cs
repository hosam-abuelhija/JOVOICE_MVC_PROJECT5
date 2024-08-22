using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JOVOICE.Models;

namespace JOVOICE.Controllers
{
    public class ElictionsDatesController : Controller
    {
        private ElectionEntities2 db = new ElectionEntities2();

        // GET: ElictionsDates
        public ActionResult Index()
        {
            return View(db.ElictionsDates.ToList());
        }

        // GET: ElictionsDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElictionsDate elictionsDate = db.ElictionsDates.Find(id);
            if (elictionsDate == null)
            {
                return HttpNotFound();
            }
            return View(elictionsDate);
        }

        // GET: ElictionsDates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ElictionsDates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,startDate,endDate")] ElictionsDate elictionsDate)
        {
            if (ModelState.IsValid)
            {
                db.ElictionsDates.Add(elictionsDate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(elictionsDate);
        }

        // GET: ElictionsDates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElictionsDate elictionsDate = db.ElictionsDates.Find(id);
            if (elictionsDate == null)
            {
                return HttpNotFound();
            }
            return View(elictionsDate);
        }

        // POST: ElictionsDates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,startDate,endDate")] ElictionsDate elictionsDate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elictionsDate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(elictionsDate);
        }

        // GET: ElictionsDates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElictionsDate elictionsDate = db.ElictionsDates.Find(id);
            if (elictionsDate == null)
            {
                return HttpNotFound();
            }
            return View(elictionsDate);
        }

        // POST: ElictionsDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ElictionsDate elictionsDate = db.ElictionsDates.Find(id);
            db.ElictionsDates.Remove(elictionsDate);
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
