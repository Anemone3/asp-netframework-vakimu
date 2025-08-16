using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Entities;

namespace com.pe.vakimu.application.Controllers.Web
{
    public class InformacionNutricionalController : Controller
    {
        private VakimuDbContext db = new VakimuDbContext();

        // GET: InformacionNutricional
        public ActionResult Index()
        {
            var informacionNutricional = db.InformacionNutricional.Include(i => i.Producto);
            return View(informacionNutricional.ToList());
        }

        // GET: InformacionNutricional/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionNutricional informacionNutricional = db.InformacionNutricional.Find(id);
            if (informacionNutricional == null)
            {
                return HttpNotFound();
            }
            return View(informacionNutricional);
        }

        // GET: InformacionNutricional/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre");
            return View();
        }

        // POST: InformacionNutricional/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,EnergiaKcal,GrasaTotal,GrasaSaturada,GrasaTrans,ColesterolMg,SodioMg,CarbohidratosTotales,FibraDietaria,AzucaresTotales,AzucaresAnhadidos,Proteina")] InformacionNutricional informacionNutricional)
        {
            if (ModelState.IsValid)
            {
                db.InformacionNutricional.Add(informacionNutricional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", informacionNutricional.IdProducto);
            return View(informacionNutricional);
        }

        // GET: InformacionNutricional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionNutricional informacionNutricional = db.InformacionNutricional.Find(id);
            if (informacionNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", informacionNutricional.IdProducto);
            return View(informacionNutricional);
        }

        // POST: InformacionNutricional/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,EnergiaKcal,GrasaTotal,GrasaSaturada,GrasaTrans,ColesterolMg,SodioMg,CarbohidratosTotales,FibraDietaria,AzucaresTotales,AzucaresAnhadidos,Proteina")] InformacionNutricional informacionNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informacionNutricional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", informacionNutricional.IdProducto);
            return View(informacionNutricional);
        }

        // GET: InformacionNutricional/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionNutricional informacionNutricional = db.InformacionNutricional.Find(id);
            if (informacionNutricional == null)
            {
                return HttpNotFound();
            }
            return View(informacionNutricional);
        }

        // POST: InformacionNutricional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformacionNutricional informacionNutricional = db.InformacionNutricional.Find(id);
            db.InformacionNutricional.Remove(informacionNutricional);
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
