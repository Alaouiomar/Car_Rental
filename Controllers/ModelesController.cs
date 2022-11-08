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
    public class ModelesController : Controller
    {
        private LocationEntities db = new LocationEntities();

        // GET: Modeles
        public ActionResult Index()
        {
            var modele = db.Modele.Include(m => m.Voiture);
            return View(modele.ToList());
        }
       
        // GET: Modeles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modele.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // GET: Modeles/Create
        public ActionResult Create()
        {
            ViewBag.idm = new SelectList(db.Voiture, "idv", "NumeroMatricule");
            return View();
        }

        // POST: Modeles/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idm,NomMarque,Serie")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Modele.Add(modele);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idm = new SelectList(db.Voiture, "idv", "NumeroMatricule", modele.idm);
            return View(modele);
        }

        // GET: Modeles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modele.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            ViewBag.idm = new SelectList(db.Voiture, "idv", "NumeroMatricule", modele.idm);
            return View(modele);
        }

        // POST: Modeles/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idm,NomMarque,Serie")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modele).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idm = new SelectList(db.Voiture, "idv", "NumeroMatricule", modele.idm);
            return View(modele);
        }

        // GET: Modeles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modele.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // POST: Modeles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modele modele = db.Modele.Find(id);
            db.Modele.Remove(modele);
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
