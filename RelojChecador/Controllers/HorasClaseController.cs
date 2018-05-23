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
    public class HorasClaseController : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();

        // GET: HorasClase
        public ActionResult Index()
        {
            return View(db.HORA_CLASE.ToList());
        }

        // GET: HorasClase/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORA_CLASE hORA_CLASE = db.HORA_CLASE.Find(id);
            if (hORA_CLASE == null)
            {
                return HttpNotFound();
            }
            return View(hORA_CLASE);
        }

        // GET: HorasClase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HorasClase/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_HORA_CLASE,HORA_INICIO,HORA_FIN")] HORA_CLASE hORA_CLASE)
        {
            if (ModelState.IsValid)
            {
                try {
                    HORA_CLASE hcAux = db.HORA_CLASE.FirstOrDefault(hc => hc.HORA_INICIO == hORA_CLASE.HORA_INICIO && hc.HORA_FIN == hORA_CLASE.HORA_FIN);

                    if (hcAux != null)
                        ModelState.AddModelError("", "La hora clase ya existe en base de datos");
                    else
                    {
                        hcAux = db.HORA_CLASE.FirstOrDefault(hc => hc.HORA_INICIO < hORA_CLASE.HORA_INICIO && hc.HORA_FIN > hORA_CLASE.HORA_INICIO);
                        if (hcAux != null)
                            ModelState.AddModelError("HORA_INICIO", "La hora de inicio no puede estar entre una hora clase ya registrada");
                        else
                        {
                            hcAux = db.HORA_CLASE.FirstOrDefault(hc => hc.HORA_INICIO < hORA_CLASE.HORA_FIN && hc.HORA_FIN > hORA_CLASE.HORA_FIN);
                            if (hcAux != null)
                                ModelState.AddModelError("HORA_FIN", "La hora de fin no puede estar entre una hora clase ya registrada");
                            else
                            {
                                db.HORA_CLASE.Add(hORA_CLASE);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }

            return View(hORA_CLASE);
        }

        // GET: HorasClase/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORA_CLASE hORA_CLASE = db.HORA_CLASE.Find(id);
            if (hORA_CLASE == null)
            {
                return HttpNotFound();
            }
            return View(hORA_CLASE);
        }

        // POST: HorasClase/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HORA_CLASE,HORA_INICIO,HORA_FIN")] HORA_CLASE hORA_CLASE)
        {
            if (ModelState.IsValid)
            {
                try {
                    HORA_CLASE hcAux = db.HORA_CLASE.FirstOrDefault(hc => hc.HORA_INICIO == hORA_CLASE.HORA_INICIO && hc.HORA_FIN == hORA_CLASE.HORA_FIN && hc.ID_HORA_CLASE != hORA_CLASE.ID_HORA_CLASE);

                    if (hcAux != null)
                        ModelState.AddModelError("", "La hora clase ya existe en base de datos");
                    else
                    {
                        hcAux = db.HORA_CLASE.FirstOrDefault(hc => hc.HORA_INICIO < hORA_CLASE.HORA_INICIO && hc.HORA_FIN > hORA_CLASE.HORA_INICIO && hc.ID_HORA_CLASE != hORA_CLASE.ID_HORA_CLASE);
                        if (hcAux != null)
                            ModelState.AddModelError("HORA_INICIO", "La hora de inicio no puede estar entre una hora clase ya registrada");
                        else
                        {
                            hcAux = db.HORA_CLASE.FirstOrDefault(hc => hc.HORA_INICIO < hORA_CLASE.HORA_FIN && hc.HORA_FIN > hORA_CLASE.HORA_FIN && hc.ID_HORA_CLASE != hORA_CLASE.ID_HORA_CLASE);
                            if (hcAux != null)
                                ModelState.AddModelError("HORA_FIN", "La hora de fin no puede estar entre una hora clase ya registrada");
                            else
                            {
                                db.Entry(hORA_CLASE).State = EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }
            return View(hORA_CLASE);
        }

        // GET: HorasClase/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORA_CLASE hORA_CLASE = db.HORA_CLASE.Find(id);
            if (hORA_CLASE == null)
            {
                return HttpNotFound();
            }
            return View(hORA_CLASE);
        }

        // POST: HorasClase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try {
                HORARIO hAux = db.HORARIO.FirstOrDefault(h => h.ID_HORA_CLASE == id);
                if (hAux == null)
                {
                    HORA_CLASE hORA_CLASE = db.HORA_CLASE.Find(id);
                    db.HORA_CLASE.Remove(hORA_CLASE);
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
