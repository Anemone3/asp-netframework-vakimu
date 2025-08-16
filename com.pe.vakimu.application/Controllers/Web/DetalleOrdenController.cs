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
    public class DetalleOrdenController : Controller
    {
        private VakimuDbContext db = new VakimuDbContext();

        // GET: DetalleOrden
        public ActionResult Index()
        {
            var detallesOrden = db.DetallesOrden.Include(d => d.Orden).Include(d => d.Producto);
            return View(detallesOrden.ToList());
        }

        // GET: DetalleOrden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            if (detalleOrden == null)
            {
                return HttpNotFound();
            }
            return View(detalleOrden);
        }

        // GET: DetalleOrden/Create
        public ActionResult Create(int ordenId)
        {
            ViewBag.OrdenId = ordenId;
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre");
            return View(new DetalleOrden { OrdenId = ordenId });
        }

        // POST: DetalleOrden/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrdenId,ProductoId,Cantidad,Descripcion,Descuento,Subtotal,Total")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
                db.DetallesOrden.Add(detalleOrden);
                db.SaveChanges();
                return RedirectToAction("Create", new { ordenId = detalleOrden.OrdenId });
            }

            ViewBag.OrdenId = detalleOrden.OrdenId;
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            if (detalleOrden == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdenId = new SelectList(db.Ordenes, "Id", "Tipo", detalleOrden.OrdenId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // POST: DetalleOrden/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrdenId,ProductoId,Cantidad,Descripcion,Descuento,Subtotal,Total")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleOrden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdenId = new SelectList(db.Ordenes, "Id", "Tipo", detalleOrden.OrdenId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            if (detalleOrden == null)
            {
                return HttpNotFound();
            }
            return View(detalleOrden);
        }

        // POST: DetalleOrden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            db.DetallesOrden.Remove(detalleOrden);
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
