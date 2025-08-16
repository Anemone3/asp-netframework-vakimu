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
    public class MetodoPagoController : Controller
    {
        private VakimuDbContext db = new VakimuDbContext();

        // GET: MetodoPago
        public ActionResult Index()
        {
            return View(db.MetodosPago.ToList());
        }

        // GET: MetodoPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodoPago metodoPago = db.MetodosPago.Find(id);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }
            return View(metodoPago);
        }

        // GET: MetodoPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetodoPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Instrucciones")] MetodoPago metodoPago)
        {
            if (ModelState.IsValid)
            {
                db.MetodosPago.Add(metodoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metodoPago);
        }

        // GET: MetodoPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodoPago metodoPago = db.MetodosPago.Find(id);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }
            return View(metodoPago);
        }

        // POST: MetodoPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Instrucciones")] MetodoPago metodoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metodoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metodoPago);
        }

        // GET: MetodoPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodoPago metodoPago = db.MetodosPago.Find(id);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }
            return View(metodoPago);
        }

        // POST: MetodoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MetodoPago metodoPago = db.MetodosPago.Find(id);
            db.MetodosPago.Remove(metodoPago);
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
