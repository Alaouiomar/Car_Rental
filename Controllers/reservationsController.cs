using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LOCATION_DES_VOITURES;

namespace LOCATION_DES_VOITURES.Controllers
{
    public class reservationsController : Controller
    {
        private LocationEntities db = new LocationEntities();

        // GET: reservations
        public ActionResult Index()
        {
            var reservation = db.reservation.Include(r => r.Client).Include(r => r.Voiture);
            return View(reservation.ToList());
        }

        // GET: reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservation reservation = db.reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: reservations/Create
        public ActionResult Create()
        {
            ViewBag.idres = new SelectList(db.Client, "idc", "NomPrenom");
            ViewBag.idres = new SelectList(db.Voiture, "idv", "NumeroMatricule");
            return View();
        }

        // POST: reservations/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idres,idc,idv,type_res,date_debut,date_fin,is_valid")] reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idres = new SelectList(db.Client, "idc", "NomPrenom", reservation.idres);
            ViewBag.idres = new SelectList(db.Voiture, "idv", "NumeroMatricule", reservation.idres);
            return View(reservation);
        }

        // GET: reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservation reservation = db.reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.idres = new SelectList(db.Client, "idc", "NomPrenom", reservation.idres);
            ViewBag.idres = new SelectList(db.Voiture, "idv", "NumeroMatricule", reservation.idres);
            return View(reservation);
        }

        // POST: reservations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idres,idc,idv,type_res,date_debut,date_fin,is_valid")] reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idres = new SelectList(db.Client, "idc", "NomPrenom", reservation.idres);
            ViewBag.idres = new SelectList(db.Voiture, "idv", "NumeroMatricule", reservation.idres);
            return View(reservation);
        }

        // GET: reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservation reservation = db.reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reservation reservation = db.reservation.Find(id);
            db.reservation.Remove(reservation);
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
