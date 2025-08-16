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
   public class OrdenController : Controller
    {
        private VakimuDbContext db = new VakimuDbContext();

        // GET: Orden
        public ActionResult Index()
        {
            var ordenes = db.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.MetodoPago)
                .Include(o => o.Sucursal);
            return View(ordenes.ToList());
        }

        // GET: Orden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
                return HttpNotFound();

            return View(orden);
        }

        // GET: Orden/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(
                db.Clientes.ToList().Select(c => new { c.Id, Nombre = c.Nombre + " " + c.Apellidos }),
                "Id", "Nombre"
            );
            ViewBag.MetodoPagoId = new SelectList(db.MetodosPago, "Id", "Nombre");
            ViewBag.SucursalId = new SelectList(db.Sucursales, "Id", "Ubicacion");
            return View();
        }

        // POST: Orden/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClienteId,SucursalId,Tipo,Estado,Descripcion,Ruc,MetodoPagoId")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                orden.Estado = "Pendiente";
                db.Ordenes.Add(orden);
                db.SaveChanges();

                // Si es delivery, registrar dirección
                if (orden.Tipo.ToLower() == "delivery")
                {
                    var direccion = new DireccionEntrega
                    {
                        OrdenId = orden.Id,
                        ClienteId = orden.ClienteId,
                        DireccionLinea1 = Request["DireccionLinea1"],
                        DireccionLinea2 = Request["DireccionLinea2"],
                        Ciudad = Request["Ciudad"],
                        Distrito = Request["Distrito"],
                        CodigoPostal = Request["CodigoPostal"],
                        Referencia = Request["Referencia"]
                    };

                    db.DireccionesEntrega.Add(direccion);
                    db.SaveChanges();
                }

                return RedirectToAction("Create", "DetalleOrden", new { ordenId = orden.Id });
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", orden.ClienteId);
            ViewBag.MetodoPagoId = new SelectList(db.MetodosPago, "Id", "Nombre", orden.MetodoPagoId);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "Id", "Ubicacion", orden.SucursalId);
            return View(orden);
        }

        // GET: Orden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
                return HttpNotFound();

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", orden.ClienteId);
            ViewBag.MetodoPagoId = new SelectList(db.MetodosPago, "Id", "Nombre", orden.MetodoPagoId);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "Id", "Ubicacion", orden.SucursalId);
            return View(orden);
        }

        // POST: Orden/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClienteId,SucursalId,Tipo,Estado,Descripcion,Ruc,MetodoPagoId")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", orden.ClienteId);
            ViewBag.MetodoPagoId = new SelectList(db.MetodosPago, "Id", "Nombre", orden.MetodoPagoId);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "Id", "Ubicacion", orden.SucursalId);
            return View(orden);
        }

        // GET: Orden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
                return HttpNotFound();

            return View(orden);
        }

        // POST: Orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orden orden = db.Ordenes.Find(id);
            db.Ordenes.Remove(orden);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
