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
    public class TempPartyCandidatesController : Controller
    {
        private ElectionEntities2 db = new ElectionEntities2();

        // GET: TempPartyCandidates
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            var tempPartyCandidates = db.TempPartyCandidates;
            return View(tempPartyCandidates.ToList());
        }

        [HttpPost]
        public ActionResult Index(int approvedCandidateId)
        {
            var approvedCandidate = db.TempPartyCandidates.Find(approvedCandidateId);
            var newOne = new PartyCandidate
            {
                birthdate = approvedCandidate.birthdate,
                candidatename = approvedCandidate.candidatename,
                partyname = approvedCandidate.partyname,
                ordercandidate = approvedCandidate.ordercandidate,
                gender = approvedCandidate.gender,
                electionarea = approvedCandidate.electionarea,
                national_id = approvedCandidate.national_id,
                email = approvedCandidate.email,
                religion = approvedCandidate.religion,
            };
            db.PartyCandidates.Add(newOne);
            db.TempPartyCandidates.Remove(approvedCandidate);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: TempPartyCandidates/Details/5
        public ActionResult Details(long? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempPartyCandidate tempPartyCandidate = db.TempPartyCandidates.Find(id);
            if (tempPartyCandidate == null)
            {
                return HttpNotFound();
            }
            return View(tempPartyCandidate);
        }

        // GET: TempPartyCandidates/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.id = new SelectList(db.PartyCandidates, "id", "partyname");
            return View();
        }

        // POST: TempPartyCandidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,national_id,electionarea,email,partyname,gender,birthdate,religion,ordercandidate,candidatename")] TempPartyCandidate tempPartyCandidate)
        {
            if (ModelState.IsValid)
            {
                db.TempPartyCandidates.Add(tempPartyCandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.PartyCandidates, "id", "partyname", tempPartyCandidate.id);
            return View(tempPartyCandidate);
        }

        // GET: TempPartyCandidates/Edit/5
        public ActionResult Edit(long? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempPartyCandidate tempPartyCandidate = db.TempPartyCandidates.Find(id);
            if (tempPartyCandidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.PartyCandidates, "id", "partyname", tempPartyCandidate.id);
            return View(tempPartyCandidate);
        }

        // POST: TempPartyCandidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,national_id,electionarea,email,partyname,gender,birthdate,religion,ordercandidate,candidatename")] TempPartyCandidate tempPartyCandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tempPartyCandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.PartyCandidates, "id", "partyname", tempPartyCandidate.id);
            return View(tempPartyCandidate);
        }

        // GET: TempPartyCandidates/Delete/5
        public ActionResult Delete(long? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempPartyCandidate tempPartyCandidate = db.TempPartyCandidates.Find(id);
            if (tempPartyCandidate == null)
            {
                return HttpNotFound();
            }
            return View(tempPartyCandidate);
        }

        // POST: TempPartyCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TempPartyCandidate tempPartyCandidate = db.TempPartyCandidates.Find(id);
            db.TempPartyCandidates.Remove(tempPartyCandidate);
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


        //Ghayda
        public ActionResult Create_ghb()
        {
            ViewBag.fk_admin = new SelectList(db.Admins, "id", "name");
            var model = new TempPartyCandidateViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_ghb(TempPartyCandidateViewModel model)
        {
            int MaxCandidates = 10;

            if (ModelState.IsValid)
            {
                int activeCandidates = 0;

                foreach (var candidate in model.Candidates)
                {
                    if (activeCandidates < MaxCandidates && candidate.national_id != null)
                    {
                        candidate.electionarea = model.ElectionArea;
                        candidate.city = model.City;
                        candidate.partyname = model.PartyName;

                        db.TempPartyCandidates.Add(candidate);
                        activeCandidates++;
                    }
                    else if (activeCandidates >= MaxCandidates)
                    {
                        break;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("candMain", "Home");
            }

            return View(model);
        }



        [HttpGet]
        public JsonResult GetUserData(int nationalNumber)
        {
            var user = db.Users.FirstOrDefault(u => u.national_id == nationalNumber);
            var localCandidate = db.LocalCandidates.FirstOrDefault(p => p.national_id == nationalNumber);

            if (user != null)
            {
                DateTime birthdate = DateTime.Parse(user.birthdate);
                var age = DateTime.Now.Year - birthdate.Year;

                if (localCandidate != null)
                {
                    return Json(new { error = "تم قبول هذا المترشح في أحد القوائم المحلية" }, JsonRequestBehavior.AllowGet);
                }
                else if (age < 25)
                {
                    return Json(new { error = "لا يُسمح بأن يكون عمر المترشح أقل من 25 عامًا" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var userData = new
                    {
                        name = user.name,
                        gender = user.gender,
                        religion = user.religion,
                        birthdate = user.birthdate,
                        email = user.email
                    };

                    return Json(userData, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { error = "هذا الرقم الوطني غير موجود" }, JsonRequestBehavior.AllowGet);
        }



    }
}
