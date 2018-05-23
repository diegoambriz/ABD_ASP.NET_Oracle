using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RelojChecador.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

namespace RelojChecador.Controllers
{
    public class ProfesoresController : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();

        // GET: Profesores
        public ActionResult Index()
        {
            return View(db.PROFESOR.ToList());
        }

        // GET: Profesores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFESOR pROFESOR = db.PROFESOR.Find(id);
            if (pROFESOR == null)
            {
                return HttpNotFound();
            }
            return View(pROFESOR);
        }

        // GET: Profesores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PROFESOR,NOMBRE1,NOMBRE2,APELLIDO1,APELLIDO2,CALLE,NO_EXTERIOR,NO_INTERIOR,LADA,TELEFONO,FECHA_NACIMIENTO,RFC,HORAS_SEMANALES,FIRMA")] PROFESOR pROFESOR)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.PROFESOR.Add(pROFESOR);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception Ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }

            return View(pROFESOR);
        }

        // GET: Profesores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFESOR pROFESOR = db.PROFESOR.Find(id);
            if (pROFESOR == null)
            {
                return HttpNotFound();
            }

            if (pROFESOR.HORAS_SEMANALES == null)
                Session["horasSemana"] = 0;
            else
                Session["horasSemana"] = pROFESOR.HORAS_SEMANALES;
            return View(pROFESOR);
        }

        // POST: Profesores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PROFESOR,NOMBRE1,NOMBRE2,APELLIDO1,APELLIDO2,CALLE,NO_EXTERIOR,NO_INTERIOR,LADA,TELEFONO,FECHA_NACIMIENTO,RFC,HORAS_SEMANALES,FIRMA")] PROFESOR pROFESOR)
        {
            try {
                if (ModelState.IsValid)
                {
                    pROFESOR.HORAS_SEMANALES = Convert.ToInt32(Session["horasSemana"]);
                    db.Entry(pROFESOR).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pROFESOR);
            }
            catch(Exception ex)
            {
                return RedirectToAction("NoConect", "Home");
            }
        }

        // GET: Profesores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFESOR pROFESOR = db.PROFESOR.Find(id);
            if (pROFESOR == null)
            {
                return HttpNotFound();
            }
            return View(pROFESOR);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try {
                HORARIO hAux = db.HORARIO.FirstOrDefault(h => h.ID_PROFESOR == id);
                if (hAux == null)
                {
                    PROFESOR pROFESOR = db.PROFESOR.Find(id);
                    db.PROFESOR.Remove(pROFESOR);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Error", "Home");
            }
            catch(Exception ex)
            {
                return RedirectToAction("NoConect", "Home");
            }
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
