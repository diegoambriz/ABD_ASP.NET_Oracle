using RelojChecador.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelojChecador.Controllers
{
    public class RelojChecadorController : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();
        // GET: RelojChecador
        public ActionResult Index()
        {
            ViewBag.idProfesor = obtenListadoProfesores();
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include="idProfesor,firma")]AsistenciaModel asistencia)
        {
            if (ModelState.IsValid)
            {
                String dia = obtenCadenaDiaHoy();
                DateTime horaActual = DateTime.Now.AddMinutes(15);
                List<HORARIO> lHorario = db.HORARIO.Where(h => h.ID_PROFESOR == asistencia.idProfesor && h.DIA_SEMANA.Contains(dia)).ToList();

                if (lHorario.Count > 0)
                {
                    int cont = 0;
                    foreach (HORARIO hAux in lHorario)
                    {
                        if (hAux.HORA_CLASE.HORA_INICIO.TimeOfDay <= DateTime.Now.AddMinutes(15).TimeOfDay && hAux.HORA_CLASE.HORA_FIN.TimeOfDay > DateTime.Now.AddMinutes(15).TimeOfDay)
                        {
                            REGISTRO_ASISTENCIA raAux = db.REGISTRO_ASISTENCIA.FirstOrDefault(ra => ra.ID_HORARIO == hAux.ID_HORARIO && ra.FECHA == DateTime.Today);
                            if (raAux == null)
                            {

                                if (hAux.PROFESOR.FIRMA == asistencia.firma)
                                {
                                    REGISTRO_ASISTENCIA rAsistencia = new REGISTRO_ASISTENCIA();
                                    rAsistencia.FECHA = DateTime.Now.Date;
                                    rAsistencia.HORA = DateTime.Now;
                                    rAsistencia.ID_HORARIO = hAux.ID_HORARIO;
                                    db.REGISTRO_ASISTENCIA.Add(rAsistencia);
                                    db.SaveChanges();
                                    db.CALCULOINSERCIONRETARDO(rAsistencia.ID_HORARIO, rAsistencia.ID_REGISTRO_ASISTENCIA, rAsistencia.HORA);
                                    db.SaveChanges();
                                    ViewBag.idProfesor = obtenListadoProfesores();
                                    return View();
                                }
                                ModelState.AddModelError("firma", "La firma es incorrecta");
                                break;
                            }
                            else
                                ModelState.AddModelError("", "Ya se registró una asistencia por parte del profesor en esta hora");
                        }
                        cont++;
                    }
                    if(cont >= lHorario.Count)
                        ModelState.AddModelError("idProfesor", "El profesor no tiene una clase asignada este día en está hora");
                }
                else
                    ModelState.AddModelError("idProfesor", "El profesor no tiene una clase asignada este día en está hora");
            }
            ViewBag.idProfesor = obtenListadoProfesores();
            return View(asistencia);
        }

        private SelectList obtenListadoProfesores()
        {
            SelectList listado;
            List<ProfesorModel> listaProfes = new List<ProfesorModel>();

            foreach (PROFESOR pAux in db.PROFESOR)
            {
                ProfesorModel prof = new ProfesorModel();
                prof.idProfesor = Convert.ToInt32(pAux.ID_PROFESOR);
                prof.displayText = pAux.NOMBRE1 + " ";
                if (!String.IsNullOrEmpty(pAux.NOMBRE2) && !String.IsNullOrWhiteSpace(pAux.NOMBRE2))
                    prof.displayText += pAux.NOMBRE2 + " ";
                prof.displayText += pAux.APELLIDO1;
                if (!String.IsNullOrEmpty(pAux.APELLIDO2) && !String.IsNullOrWhiteSpace(pAux.APELLIDO2))
                    prof.displayText += " " + pAux.APELLIDO2;

                listaProfes.Add(prof);
            }

            listado = new SelectList(listaProfes, "idProfesor", "displayText");

            return listado;
        }

        private String obtenCadenaDiaHoy()
        {
            String dia = "";

            switch(DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dia = "L";
                    break;
                case DayOfWeek.Tuesday:
                    dia = "Ma";
                    break;
                case DayOfWeek.Wednesday:
                    dia = "Mi";
                    break;
                case DayOfWeek.Thursday:
                    dia = "J";
                    break;
                case DayOfWeek.Friday:
                    dia = "V";
                    break;
                default:
                    dia = "D";
                    break;
            }

            return dia;
        }
    }
}