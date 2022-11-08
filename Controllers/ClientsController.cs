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
    public class ClientsController : Controller
    {
        private LocationEntities db = new LocationEntities();

        // GET: Clients
        public ActionResult Index()
        {
            var client = db.Client.Include(c => c.reservation);
            return View(client.ToList());
        }
       
        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.idc = new SelectList(db.reservation, "idres", "type_res");
            return View();
        }

        // POST: Clients/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client, HttpPostedFileBase imgfile, HttpPostedFileBase imgcin)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                string path1 = "";

                if (imgfile.FileName.Length > 0)
                {
                    path = "~/Photos/" + Path.GetFileName(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath(path));
                }

                if (imgcin.FileName.Length > 0) {
                    path1 = "~/Photos/" + Path.GetFileName(imgcin.FileName);
                    imgcin.SaveAs(Server.MapPath(path1));
                }
                client.permis = path;
                client.Cin = path1;
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.idc = new SelectList(db.reservation, "idres", "type_res", client.idc);
            return View(client);
        }

        // POST: Clients/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idc,NomPrenom,Email,Numero,DateNaissance,password,Cin,is_valid,permis,isadmin")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idc = new SelectList(db.reservation, "idres", "type_res", client.idc);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            db.Client.Remove(client);
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,password")] Client client)
        {
            var rec = db.Client.Where(x => x.Email == client.Email && x.password == client.password).ToList().FirstOrDefault();
            if (rec != null)
            {
                Session["userName"] = rec.NomPrenom;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid User";
                return View(client);
            }
          
        }
    }
}
