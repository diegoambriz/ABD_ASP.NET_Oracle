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
    public class GruposController : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();

        // GET: Grupos
        public ActionResult Index()
        {
            return View(db.GRUPO.ToList());
        }

        // GET: Grupos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GRUPO gRUPO = db.GRUPO.Find(id);
            if (gRUPO == null)
            {
                return HttpNotFound();
            }
            return View(gRUPO);
        }

        // GET: Grupos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grupos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_GRUPO,CICLO_ESCOLAR,SEMESTRE,GRUPO1")] GRUPO gRUPO)
        {
            if (ModelState.IsValid)
            {
                try {
                    GRUPO gAux = db.GRUPO.FirstOrDefault(g => g.CICLO_ESCOLAR == gRUPO.CICLO_ESCOLAR && g.SEMESTRE == gRUPO.SEMESTRE && g.GRUPO1 == gRUPO.GRUPO1);

                    if (gAux == null)
                    {
                        db.GRUPO.Add(gRUPO);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "El grupo ya existe en la base de datos");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }

            return View(gRUPO);
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GRUPO gRUPO = db.GRUPO.Find(id);
            if (gRUPO == null)
            {
                return HttpNotFound();
            }
            return View(gRUPO);
        }

        // POST: Grupos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_GRUPO,CICLO_ESCOLAR,SEMESTRE,GRUPO1")] GRUPO gRUPO)
        {
            if (ModelState.IsValid)
            {
                try {
                    GRUPO gAux = db.GRUPO.FirstOrDefault(g => g.CICLO_ESCOLAR == gRUPO.CICLO_ESCOLAR && g.SEMESTRE == gRUPO.SEMESTRE && g.GRUPO1 == gRUPO.GRUPO1 && g.ID_GRUPO != gRUPO.ID_GRUPO);

                    if (gAux == null)
                    {
                        db.Entry(gRUPO).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "El grupo ya existe en la base de datos");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }
            return View(gRUPO);
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GRUPO gRUPO = db.GRUPO.Find(id);
            if (gRUPO == null)
            {
                return HttpNotFound();
            }
            return View(gRUPO);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try {
                HORARIO hAux = db.HORARIO.FirstOrDefault(h => h.ID_GRUPO == id);
                if (hAux == null)
                {
                    GRUPO gRUPO = db.GRUPO.Find(id);
                    db.GRUPO.Remove(gRUPO);
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
