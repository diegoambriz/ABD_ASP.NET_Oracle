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
    public class MateriasController : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();

        // GET: Materias
        public ActionResult Index()
        {
            return View(db.MATERIA.ToList());
        }

        // GET: Materias/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATERIA mATERIA = db.MATERIA.Find(id);
            if (mATERIA == null)
            {
                return HttpNotFound();
            }
            return View(mATERIA);
        }

        // GET: Materias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_MATERIA,NOMBRE,NUM_HORAS_CLASE,SEMESTRE")] MATERIA mATERIA)
        {
            if (ModelState.IsValid)
            {
                try {
                    MATERIA mat = db.MATERIA.FirstOrDefault(m => m.NOMBRE == mATERIA.NOMBRE);
                    if (mat == null)
                    {
                        db.MATERIA.Add(mATERIA);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("NOMBRE", "El nombre de la materia ya existe en base de datos");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }

            return View(mATERIA);
        }

        // GET: Materias/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATERIA mATERIA = db.MATERIA.Find(id);
            if (mATERIA == null)
            {
                return HttpNotFound();
            }
            return View(mATERIA);
        }

        // POST: Materias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_MATERIA,NOMBRE,NUM_HORAS_CLASE,SEMESTRE")] MATERIA mATERIA)
        {
            if (ModelState.IsValid)
            {
                try {
                    MATERIA mAux = db.MATERIA.FirstOrDefault(m => m.NOMBRE == mATERIA.NOMBRE && m.ID_MATERIA != mATERIA.ID_MATERIA);

                    if (mAux == null)
                    {
                        db.Entry(mATERIA).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("NOMBRE", "El nombre de la materia ya existe en la base de datos");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }
            return View(mATERIA);
        }

        // GET: Materias/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATERIA mATERIA = db.MATERIA.Find(id);
            if (mATERIA == null)
            {
                return HttpNotFound();
            }
            return View(mATERIA);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                HORARIO hAux = db.HORARIO.FirstOrDefault(h => h.ID_MATERIA == id);
                if (hAux == null)
                {
                    MATERIA mATERIA = db.MATERIA.Find(id);
                    db.MATERIA.Remove(mATERIA);
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
