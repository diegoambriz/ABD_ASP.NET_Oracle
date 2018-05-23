using RelojChecador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RelojChecador.Controllers
{
    public class HorariosController : Controller
    {
        private ChecadorEntities db = new ChecadorEntities();
        // GET: Horarios
        public ActionResult Index()
        {
            List<HorarioModel> listaHorarios = new List<HorarioModel>();

            foreach(HORARIO hAux in db.HORARIO)
            {
                HorarioModel h = this.cargaDatosHorario(hAux);
                listaHorarios.Add(h);
            }

            return View(listaHorarios);
        }

        // GET: Horarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIO hAux = db.HORARIO.Find(id);
            if(hAux == null)
            {
                return HttpNotFound();
            }
            HorarioModel horario = cargaDatosHorario(hAux);
            return View(horario);
        }

        // GET: Horarios/Create
        public ActionResult Create()
        {
            this.cargaCombosListados();
            return View();
        }

        // POST: Horarios/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "idGrupo,idMateria,idProfesor,idHoraClase,Lunes,Martes,Miercoles,Jueves,Viernes")] HorarioModel horario)
        {
            if (ModelState.IsValid)
            {
                try {
                    String msjError = "";
                    if (horario.Lunes || horario.Martes || horario.Miercoles || horario.Jueves || horario.Viernes)
                    {
                        if (validaDatosUnicosGrupo(horario, ref msjError))
                        {
                            if (validaDatosUnicosProfesor(horario, ref msjError))
                            {
                                HORARIO hAlta = new HORARIO();
                                hAlta.ID_GRUPO = horario.idGrupo;
                                hAlta.ID_MATERIA = horario.idMateria;
                                hAlta.ID_PROFESOR = horario.idProfesor;
                                hAlta.ID_HORA_CLASE = horario.idHoraClase;
                                hAlta.DIA_SEMANA = this.generaCadenaDias(horario);

                                db.HORARIO.Add(hAlta);
                                db.SaveChanges();
                                db.INCREMENTOHORASPROFESOR(hAlta.ID_PROFESOR, hAlta.DIA_SEMANA);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }

                        ModelState.AddModelError("", msjError);
                    }
                    else
                        ModelState.AddModelError("", "Debe seleccionar al menos un día de la semana");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }

            this.cargaCombosListados();
            return View();
        }

        // GET: Horarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HORARIO hAux = db.HORARIO.Find(id);
            if (hAux == null)
                return HttpNotFound();

            HorarioModel horario = new HorarioModel();
            horario.idHorario = Convert.ToInt32(hAux.ID_HORARIO);
            horario.idGrupo = Convert.ToInt32(hAux.ID_GRUPO);
            horario.idMateria = Convert.ToInt32(hAux.ID_MATERIA);
            horario.idProfesor = Convert.ToInt32(hAux.ID_PROFESOR);
            horario.idHoraClase = Convert.ToInt32(hAux.ID_HORA_CLASE);

            this.cargaDiasSemana(ref horario, hAux);
            this.cargaCombosListados();
            return View(horario);
        }

        // POST: Horarios/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "idHorario,idGrupo,idMateria,idProfesor,idHoraClase,Lunes,Martes,Miercoles,Jueves,Viernes")] HorarioModel horario)
        {
            if (ModelState.IsValid)
            {
                try {
                    if (horario.Lunes || horario.Martes || horario.Miercoles || horario.Jueves || horario.Viernes)
                    {
                        String msjError = "";
                        if (validaDatosUnicosGrupo(horario, ref msjError))
                        {
                            if (validaDatosUnicosProfesor(horario, ref msjError))
                            {
                                HORARIO hEditado = db.HORARIO.Find(horario.idHorario);
                                String diasAnt = hEditado.DIA_SEMANA;
                                hEditado.ID_GRUPO = horario.idGrupo;
                                hEditado.ID_MATERIA = horario.idMateria;
                                hEditado.ID_PROFESOR = horario.idProfesor;
                                hEditado.ID_HORA_CLASE = horario.idHoraClase;
                                hEditado.DIA_SEMANA = generaCadenaDias(horario);
                                db.Entry(hEditado).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                db.INCDECHORASPROFESOR(hEditado.ID_PROFESOR, hEditado.DIA_SEMANA, diasAnt);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }

                        ModelState.AddModelError("", msjError);
                    }
                    else
                        ModelState.AddModelError("", "Debe seleccionar al menos un día de la semana");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("NoConect", "Home");
                }
            }

            return View();
        }

        // GET: Horarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIO hAux = db.HORARIO.Find(id);
            if(hAux == null)
            {
                return HttpNotFound();
            }
            HorarioModel horario = this.cargaDatosHorario(hAux);
            return View(horario);
        }

        // POST: Horarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try {
                REGISTRO_ASISTENCIA raAux = db.REGISTRO_ASISTENCIA.FirstOrDefault(ra => ra.ID_HORARIO == id);
                if (raAux == null)
                {
                    HORARIO horario = db.HORARIO.Find(id);
                    String diasAnt = horario.DIA_SEMANA;
                    db.HORARIO.Remove(horario);
                    db.SaveChanges();
                    db.DECHORASPROFESOR(id, diasAnt);
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

        private HorarioModel cargaDatosHorario(HORARIO hAux)
        {
            HorarioModel h = new HorarioModel();
            h.idHorario = Convert.ToInt32(hAux.ID_HORARIO);
            h.txtGrupo = hAux.GRUPO.CICLO_ESCOLAR + "-" + hAux.GRUPO.SEMESTRE.ToString() + hAux.GRUPO.GRUPO1.ToString();
            h.txtMateria = hAux.MATERIA.NOMBRE;
            this.cargaNombreProfesor(ref h, hAux);
            h.txtHoraClase = hAux.HORA_CLASE.HORA_INICIO.ToString() + " - " + hAux.HORA_CLASE.HORA_FIN.ToString();
            h.diasSemana = hAux.DIA_SEMANA;

            return h;
        }

        private void cargaNombreProfesor(ref HorarioModel h, HORARIO hAux)
        {
            h.txtProfesor = hAux.PROFESOR.NOMBRE1 + " ";
            if (!String.IsNullOrEmpty(hAux.PROFESOR.NOMBRE2) && !String.IsNullOrWhiteSpace(hAux.PROFESOR.NOMBRE2))
                h.txtProfesor += hAux.PROFESOR.NOMBRE2 + " ";
            h.txtProfesor += hAux.PROFESOR.APELLIDO1;
            if (!String.IsNullOrEmpty(hAux.PROFESOR.APELLIDO2) && !String.IsNullOrWhiteSpace(hAux.PROFESOR.APELLIDO2))
                h.txtProfesor += " " + hAux.PROFESOR.APELLIDO2;
        }

        private void cargaDiasSemana(ref HorarioModel h, HORARIO hAux)
        {
            if (hAux.DIA_SEMANA.Contains("L"))
                h.Lunes = true;
            if (hAux.DIA_SEMANA.Contains("Ma"))
                h.Martes = true;
            if (hAux.DIA_SEMANA.Contains("Mi"))
                h.Miercoles = true;
            if (hAux.DIA_SEMANA.Contains("J"))
                h.Jueves = true;
            if (hAux.DIA_SEMANA.Contains("V"))
                h.Viernes = true;
        }

        private void cargaCombosListados()
        {
            ViewBag.idGrupo = obtenListadoGrupos();
            ViewBag.idMateria = obtenListadoMaterias();
            ViewBag.idProfesor = obtenListadoProfesores();
            ViewBag.idHoraClase = obtenListadoHorasClase();
        }

        private void cargaCombosListados(HORARIO hAux)
        {
            ViewBag.idGrupo = obtenListadoGrupos(hAux);
            ViewBag.idMateria = obtenListadoMaterias(hAux);
            ViewBag.idProfesor = obtenListadoProfesores(hAux);
            ViewBag.idHoraClase = obtenListadoHorasClase(hAux);
        }

        private SelectList obtenListadoGrupos()
        {
            SelectList listado;
            List<GrupoModel> listaGrupos = new List<GrupoModel>();

            foreach(GRUPO gAux in db.GRUPO)
            {
                GrupoModel gpo = new GrupoModel();
                gpo.idGrupo = Convert.ToInt32(gAux.ID_GRUPO);
                gpo.textoDisplay = gAux.CICLO_ESCOLAR + " - " + gAux.SEMESTRE.ToString() + gAux.GRUPO1.ToString();
                listaGrupos.Add(gpo);
            }

            listado = new SelectList(listaGrupos, "idGrupo", "textoDisplay");

            return listado;
        }

        private SelectList obtenListadoGrupos(HORARIO hAux)
        {
            SelectList listado;
            List<GrupoModel> listaGrupos = new List<GrupoModel>();

            foreach (GRUPO gAux in db.GRUPO)
            {
                GrupoModel gpo = new GrupoModel();
                gpo.idGrupo = Convert.ToInt32(gAux.ID_GRUPO);
                gpo.textoDisplay = gAux.CICLO_ESCOLAR + " - " + gAux.SEMESTRE.ToString() + gAux.GRUPO1.ToString();
                listaGrupos.Add(gpo);
            }

            listado = new SelectList(listaGrupos, "idGrupo", "textoDisplay", hAux.ID_GRUPO);

            return listado;
        }

        private SelectList obtenListadoMaterias()
        {
            SelectList listado;
            List<MateriaModel> listaMaterias = new List<MateriaModel>();

            foreach (MATERIA mAux in db.MATERIA)
            {
                MateriaModel mat = new MateriaModel();
                mat.idMateria = Convert.ToInt32(mAux.ID_MATERIA);
                mat.displayText = mAux.NOMBRE;
                listaMaterias.Add(mat);
            }

            listado = new SelectList(listaMaterias, "idMateria", "displayText");

            return listado;
        }

        private SelectList obtenListadoMaterias(HORARIO hAux)
        {
            SelectList listado;
            List<MateriaModel> listaMaterias = new List<MateriaModel>();

            foreach(MATERIA mAux in db.MATERIA)
            {
                MateriaModel mat = new MateriaModel();
                mat.idMateria = Convert.ToInt32(mAux.ID_MATERIA);
                mat.displayText = mAux.NOMBRE;
                listaMaterias.Add(mat);
            }

            listado = new SelectList(listaMaterias, "idMateria", "displayText", hAux.ID_MATERIA);

            return listado;
        }

        private SelectList obtenListadoProfesores()
        {
            SelectList listado;
            List<ProfesorModel> listaProfes = new List<ProfesorModel>();

            foreach(PROFESOR pAux in db.PROFESOR)
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

        private SelectList obtenListadoProfesores(HORARIO hAux)
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

            listado = new SelectList(listaProfes, "idProfesor", "displayText", hAux.ID_PROFESOR);

            return listado;
        }

        private SelectList obtenListadoHorasClase()
        {
            SelectList listado;
            List<HoraClaseModel> listaHorasClase = new List<HoraClaseModel>();

            foreach(HORA_CLASE hcAux in db.HORA_CLASE)
            {
                HoraClaseModel hc = new HoraClaseModel();
                hc.idHoraClase = Convert.ToInt32(hcAux.ID_HORA_CLASE);
                hc.displayText = hcAux.HORA_INICIO.ToString("HH:mm") + " - " + hcAux.HORA_FIN.ToString("HH:mm");
                listaHorasClase.Add(hc);
            }

            listado = new SelectList(listaHorasClase, "idHoraClase", "displayText");

            return listado;
        }

        private SelectList obtenListadoHorasClase(HORARIO hAux)
        {
            SelectList listado;
            List<HoraClaseModel> listaHorasClase = new List<HoraClaseModel>();

            foreach (HORA_CLASE hcAux in db.HORA_CLASE)
            {
                HoraClaseModel hc = new HoraClaseModel();
                hc.idHoraClase = Convert.ToInt32(hcAux.ID_HORA_CLASE);
                hc.displayText = hcAux.HORA_INICIO.ToString("HH:mm") + " - " + hcAux.HORA_FIN.ToString("HH:mm");
                listaHorasClase.Add(hc);
            }

            listado = new SelectList(listaHorasClase, "idHoraClase", "displayText", hAux.ID_HORA_CLASE);

            return listado;
        }

        private String generaCadenaDias(HorarioModel horario)
        {
            String dias = "";

            if (horario.Lunes)
                dias += "L";
            if (horario.Martes)
                dias += "Ma";
            if (horario.Miercoles)
                dias += "Mi";
            if (horario.Jueves)
                dias += "J";
            if (horario.Viernes)
                dias += "V";

            return dias;
        }

        private int cuentaDiasSemana(HorarioModel h)
        {
            int contDias = 0;

            if (h.Lunes)
                contDias++;
            if (h.Martes)
                contDias++;
            if (h.Miercoles)
                contDias++;
            if (h.Jueves)
                contDias++;
            if (h.Viernes)
                contDias++;

            return contDias;
        }

        private int cuentaDiasSemana(String dias)
        {
            int contDias = 0;

            if (dias.Contains("L"))
                contDias++;
            if (dias.Contains("M"))
                contDias++;
            if (dias.Contains("M"))
                contDias++;
            if (dias.Contains("J"))
                contDias++;
            if (dias.Contains("V"))
                contDias++;

            return contDias;
        }

        private bool validaDatosUnicosGrupo(HorarioModel horario, ref String msjError)
        {
            bool valido = true;

            HORARIO hBuscar = db.HORARIO.FirstOrDefault(h => h.ID_GRUPO == horario.idGrupo && h.ID_MATERIA == horario.idMateria && h.ID_HORARIO != horario.idHorario);
            MATERIA mBuscar = db.MATERIA.FirstOrDefault(m => m.ID_MATERIA == horario.idMateria);
            int horasAsignar = cuentaDiasSemana(horario);
            int numHoras = Convert.ToInt32(mBuscar.NUM_HORAS_CLASE);

            if (hBuscar != null)
            {
                int auxHoras = cuentaDiasSemana(hBuscar.DIA_SEMANA);

                if ((auxHoras + horasAsignar) > numHoras)
                {
                    valido = false;
                    if (auxHoras == numHoras)
                        msjError = "El grupo ya tiene asignadas todas las horas de la materia";
                    else
                        msjError = "El grupo se pasa con " + ((auxHoras + horasAsignar) - numHoras) + " horas para esta materia";
                }
                else
                {
                    if (!comparaDiasHoraClase(hBuscar.DIA_SEMANA, horario, true, ref msjError))
                        valido = false;
                }
            }
            else
            {
                if (horasAsignar > numHoras)
                {
                    valido = false;
                    msjError = "El grupo se pasa con " + (horasAsignar - numHoras) + " horas para esta materia";
                }
            }

            return valido;
        }

        private bool validaDatosUnicosProfesor(HorarioModel horario, ref String msjError)
        {
            List<HORARIO> hBuscar = db.HORARIO.Where(h => h.ID_HORA_CLASE == horario.idHoraClase && h.ID_PROFESOR == horario.idProfesor && h.ID_HORARIO != horario.idHorario).ToList();

            foreach(HORARIO h in hBuscar)
            {
                if (!comparaDiasHoraClase(h.DIA_SEMANA, horario, false, ref msjError))
                    return false;
            }

            return true;
        }

        private bool comparaDiasHoraClase(String cadDias, HorarioModel horario, bool grupo, ref String msjError)
        {
            if(cadDias.Contains("L") && horario.Lunes)
            {
                if (grupo)
                    msjError = "El grupo ya tiene una materia asignada ";
                else
                    msjError = "El profesor ya tiene un grupo asignado ";
                msjError += "el Lunes en la hora-clase seleccionada";
                return false;
            }
            if (cadDias.Contains("Ma") && horario.Martes)
            {
                if (grupo)
                    msjError = "El grupo ya tiene una materia asignada ";
                else
                    msjError = "El profesor ya tiene un grupo asignado ";
                msjError += "el Martes en la hora-clase seleccionada";
                return false;
            }
            if (cadDias.Contains("Mi") && horario.Miercoles)
            {
                if (grupo)
                    msjError = "El grupo ya tiene una materia asignada ";
                else
                    msjError = "El profesor ya tiene un grupo asignado ";
                msjError += "el Miercoles en la hora-clase seleccionada";
                return false;
            }
            if (cadDias.Contains("J") && horario.Jueves)
            {
                if (grupo)
                    msjError = "El grupo ya tiene una materia asignada ";
                else
                    msjError = "El profesor ya tiene un grupo asignado ";
                msjError += "el Jueves en la hora-clase seleccionada";
                return false;
            }
            if (cadDias.Contains("V") && horario.Viernes)
            {
                if (grupo)
                    msjError = "El grupo ya tiene una materia asignada ";
                else
                    msjError = "El profesor ya tiene un grupo asignado ";
                msjError += "el Viernes en la hora-clase seleccionada";
                return false;
            }

            return true;
        }
    }
}
