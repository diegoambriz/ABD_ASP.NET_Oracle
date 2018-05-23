using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelojChecador.Models
{
    public class HorarioModel
    {
        public int idHorario { get; set; }

        [Display(Name="Grupo")]
        public String txtGrupo { get; set; }
        [Required(ErrorMessage="Debe seleccionar un grupo")]
        [Display(Name = "Grupo")]
        public int idGrupo { get; set; }

        [Display(Name="Materia")]
        public String txtMateria { get; set; }
        [Required(ErrorMessage="Debe seleccionar una materia")]
        [Display(Name="Materia")]
        public int idMateria { get; set; }

        [Display(Name="Profesor")]
        public String txtProfesor { get; set; }
        [Required(ErrorMessage="Debe seleccionar un profesor")]
        [Display(Name = "Profesor")]
        public int idProfesor { get; set; }

        [Display(Name="Hora-Clase")]
        public String txtHoraClase { get; set; }
        [Required(ErrorMessage="Debe seleccionar una hora-clase")]
        [Display(Name = "Hora-Clase")]
        public int idHoraClase { get; set; }

        [Display(Name="Dias")]
        public String diasSemana { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }

        public HorarioModel()
        {

        }
    }
}
