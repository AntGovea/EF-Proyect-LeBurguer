using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class MateriasController : Controller
    {
        private ConexionDBContext db = new ConexionDBContext();

        // GET: Materias
        public ActionResult Index()
        {
            return View(db.Materias.ToList());
        }

        // GET: Materias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaPrima materiaPrima = db.Materias.Find(id);
            if (materiaPrima == null)
            {
                return HttpNotFound();
            }
            return View(materiaPrima);
        }

        // GET: Materias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MateriaPrimaId,Nombre,Cantidad,Unidad")] MateriaPrima materiaPrima)
        {
            if (ModelState.IsValid)
            {
                db.Materias.Add(materiaPrima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materiaPrima);
        }

        // GET: Materias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaPrima materiaPrima = db.Materias.Find(id);
            if (materiaPrima == null)
            {
                return HttpNotFound();
            }
            return View(materiaPrima);
        }

        // POST: Materias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MateriaPrimaId,Nombre,Cantidad,Unidad")] MateriaPrima materiaPrima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materiaPrima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materiaPrima);
        }

        // GET: Materias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaPrima materiaPrima = db.Materias.Find(id);
            if (materiaPrima == null)
            {
                return HttpNotFound();
            }
            return View(materiaPrima);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MateriaPrima materiaPrima = db.Materias.Find(id);
            db.Materias.Remove(materiaPrima);
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
