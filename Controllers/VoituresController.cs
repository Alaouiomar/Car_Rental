using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LOCATION_DES_VOITURES;

namespace LOCATION_DES_VOITURES.Controllers
{
    public class VoituresController : Controller
    {
        private LocationEntities db = new LocationEntities();
        // GET: Voitures
      
        public ActionResult Index()
        {
            var voiture = db.Voiture.Include(v => v.Categorie).Include(v => v.Modele).Include(v => v.reservation);
            return View(voiture.ToList());
        }
        

        // GET: Voitures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // GET: Voitures/Create

        public ActionResult Create()
        {

            ViewBag.categorie = db.Categorie.ToList();
            ViewBag.modele = db.Modele.ToList().Select(x => new { x.idm, nom = x.NomMarque + " " + x.Serie });
            return View();
        }
        [HttpPost]
        public ActionResult Create(Voiture voiture, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                string path = "";

                if (imgfile.FileName.Length > 0)
                {
                    path = "~/Photos/" + Path.GetFileName(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath(path));
                }
                voiture.Vimage = path;
                db.Voiture.Add(voiture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voiture);
        }

        // GET: Voitures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            ViewBag.idv = new SelectList(db.Categorie, "idcat", "NomCat", voiture.idv);
            ViewBag.idv = new SelectList(db.Modele, "idm", "NomMarque", voiture.idv);
            ViewBag.idv = new SelectList(db.reservation, "idres", "type_res", voiture.idv);
            return View(voiture);
        }

        // POST: Voitures/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idv,NumeroMatricule,DateCirculation,Carburant,Prix,Vimage,idcat,idm")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idv = new SelectList(db.Categorie, "idcat", "NomCat", voiture.idv);
            ViewBag.idv = new SelectList(db.Modele, "idm", "NomMarque", voiture.idv);
            ViewBag.idv = new SelectList(db.reservation, "idres", "type_res", voiture.idv);
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voiture voiture = db.Voiture.Find(id);
            db.Voiture.Remove(voiture);
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
        public ActionResult HomePage()
        {
            var recs = db.Voiture.ToList();
            return View(recs);
        }
    }
}
