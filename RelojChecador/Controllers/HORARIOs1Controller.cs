using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RelojChecador.Models;

namespace RelojChecador.Controllers
{
    public class HORARIOs1Controller : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();

        // GET: HORARIOs1
        public ActionResult Index()
        {
            var hORARIO = db.HORARIO.Include(h => h.GRUPO).Include(h => h.HORA_CLASE).Include(h => h.MATERIA).Include(h => h.PROFESOR);
            return View(hORARIO.ToList());
        }

        // GET: HORARIOs1/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIO hORARIO = db.HORARIO.Find(id);
            if (hORARIO == null)
            {
                return HttpNotFound();
            }
            return View(hORARIO);
        }

        // GET: HORARIOs1/Create
        public ActionResult Create()
        {
            ViewBag.ID_GRUPO = new SelectList(db.GRUPO, "ID_GRUPO", "CICLO_ESCOLAR");
            ViewBag.ID_HORA_CLASE = new SelectList(db.HORA_CLASE, "ID_HORA_CLASE", "ID_HORA_CLASE");
            ViewBag.ID_MATERIA = new SelectList(db.MATERIA, "ID_MATERIA", "NOMBRE");
            ViewBag.ID_PROFESOR = new SelectList(db.PROFESOR, "ID_PROFESOR", "NOMBRE1");
            return View();
        }

        // POST: HORARIOs1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_HORARIO,ID_HORA_CLASE,ID_GRUPO,ID_MATERIA,ID_PROFESOR,DIA_SEMANA")] HORARIO hORARIO)
        {
            if (ModelState.IsValid)
            {
                db.HORARIO.Add(hORARIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_GRUPO = new SelectList(db.GRUPO, "ID_GRUPO", "CICLO_ESCOLAR", hORARIO.ID_GRUPO);
            ViewBag.ID_HORA_CLASE = new SelectList(db.HORA_CLASE, "ID_HORA_CLASE", "ID_HORA_CLASE", hORARIO.ID_HORA_CLASE);
            ViewBag.ID_MATERIA = new SelectList(db.MATERIA, "ID_MATERIA", "NOMBRE", hORARIO.ID_MATERIA);
            ViewBag.ID_PROFESOR = new SelectList(db.PROFESOR, "ID_PROFESOR", "NOMBRE1", hORARIO.ID_PROFESOR);
            return View(hORARIO);
        }

        // GET: HORARIOs1/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIO hORARIO = db.HORARIO.Find(id);
            if (hORARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_GRUPO = new SelectList(db.GRUPO, "ID_GRUPO", "CICLO_ESCOLAR", hORARIO.ID_GRUPO);
            ViewBag.ID_HORA_CLASE = new SelectList(db.HORA_CLASE, "ID_HORA_CLASE", "ID_HORA_CLASE", hORARIO.ID_HORA_CLASE);
            ViewBag.ID_MATERIA = new SelectList(db.MATERIA, "ID_MATERIA", "NOMBRE", hORARIO.ID_MATERIA);
            ViewBag.ID_PROFESOR = new SelectList(db.PROFESOR, "ID_PROFESOR", "NOMBRE1", hORARIO.ID_PROFESOR);
            return View(hORARIO);
        }

        // POST: HORARIOs1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HORARIO,ID_HORA_CLASE,ID_GRUPO,ID_MATERIA,ID_PROFESOR,DIA_SEMANA")] HORARIO hORARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hORARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_GRUPO = new SelectList(db.GRUPO, "ID_GRUPO", "CICLO_ESCOLAR", hORARIO.ID_GRUPO);
            ViewBag.ID_HORA_CLASE = new SelectList(db.HORA_CLASE, "ID_HORA_CLASE", "ID_HORA_CLASE", hORARIO.ID_HORA_CLASE);
            ViewBag.ID_MATERIA = new SelectList(db.MATERIA, "ID_MATERIA", "NOMBRE", hORARIO.ID_MATERIA);
            ViewBag.ID_PROFESOR = new SelectList(db.PROFESOR, "ID_PROFESOR", "NOMBRE1", hORARIO.ID_PROFESOR);
            return View(hORARIO);
        }

        // GET: HORARIOs1/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIO hORARIO = db.HORARIO.Find(id);
            if (hORARIO == null)
            {
                return HttpNotFound();
            }
            return View(hORARIO);
        }

        // POST: HORARIOs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HORARIO hORARIO = db.HORARIO.Find(id);
            db.HORARIO.Remove(hORARIO);
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
