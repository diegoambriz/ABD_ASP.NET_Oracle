using System;
using System.ComponentModel.DataAnnotations;

namespace RelojChecador.Models
{
    public class MetaDatosProfesor
    {
        [StringLength(50, ErrorMessage = "El tamaño máximo son 50 caracteres")]
        [Required(ErrorMessage = "Primer nombre es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Primer Nombre", ShortName = "1er. Nombre")]
        public String NOMBRE1;

        [StringLength(50, ErrorMessage = "El tamaño máximo son 50 caracteres")]
        [Display(Name = "Segundo Nombre", ShortName = "2o. Nombre")]
        public String NOMBRE2;

        [StringLength(50, ErrorMessage = "El tamaño máximo son 50 caracteres")]
        [Required(ErrorMessage = "Primer apellido es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Primer Apellido", ShortName = "1er. Apellido")]
        public String APELLIDO1;

        [StringLength(50, ErrorMessage = "El tamaño máximo son 50 caracteres")]
        [Display(Name = "Segundo Apellido", ShortName = "2o. Apellido")]
        public String APELLIDO2;

        [StringLength(100, ErrorMessage = "El tamaño máximo son 100 caracteres")]
        [Required(ErrorMessage = "Calle es obligatoria", AllowEmptyStrings = false)]
        [Display(Name = "Calle")]
        public String CALLE;

        [StringLength(10, ErrorMessage = "El tamaño máximo son 10 caracteres")]
        [Required(ErrorMessage = "Número exterior es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "No. Exterior", ShortName = "No. Ext.")]
        public String NO_EXTERIOR;

        [StringLength(10, ErrorMessage = "El tamaño máximo son 10 caracteres")]
        [Display(Name = "No. Interior", ShortName = "No. Int.")]
        public String NO_INTERIOR;

        [Required(ErrorMessage = "Lada es obligatoria", AllowEmptyStrings = false)]
        [Display(Name = "Lada")]
        public int LADA;

        
        [Required(ErrorMessage = "Teléfono es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Teléfono", ShortName = "Tel.")]
        public long TELEFONO;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Fecha de nacimiento es obligatoria", AllowEmptyStrings = false)]
        [Display(Name = "Fecha Nacimiento", ShortName = "F. Nac.")]
        public DateTime FECHA_NACIMIENTO;

        [StringLength(13, MinimumLength = 13, ErrorMessage = "El tamaño debe ser 13 caracteres")]
        [Required(ErrorMessage = "RFC es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "RFC")]
        public String RFC;

        [Display(Name = "Clases-Semana")]
        public int HORAS_SEMANALES;

        [StringLength(20, ErrorMessage = "El tamaño máximo son 20 caracteres")]
        [Required(ErrorMessage = "Firma es obligatoria", AllowEmptyStrings = false)]
        [Display(Name = "Firma")]
        public String FIRMA;
    }

    public class MetaDatosMateria
    {
        [StringLength(100, ErrorMessage = "El tamaño máximo son 100 caracteres")]
        [Required(ErrorMessage = "Nombre de materia es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Nombre")]
        public String NOMBRE;

        [Range(1, 5, ErrorMessage = "Número de horas semanales debe estar de 1 a 5")]
        [Required(ErrorMessage = "Número de horas semanales es obligatorio")]
        [Display(Name = "Horas-Clase")]
        public int NUM_HORAS_CLASE;

        [Range(1, 6, ErrorMessage = "El semestre debe estar de 1 a 6")]
        [Required(ErrorMessage = "Semestre es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Semestre")]
        public int SEMESTRE;
    }

    public class MetaDatosGrupo
    {
        [StringLength(50, ErrorMessage = "El tamaño máximo son 50 caracteres")]
        [Required(ErrorMessage = "Ciclo escolar es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Ciclo Escolar", ShortName = "C. Escolar")]
        public String CICLO_ESCOLAR;

        [Range(1, 6, ErrorMessage = "El semestre debe estar de 1 a 6")]
        [Required(ErrorMessage = "Semestre es obligatorio", AllowEmptyStrings = false)]
        [Display(Name = "Semestre")]
        public int SEMESTRE;

        [Required(ErrorMessage = "Grupo es obligatorio ", AllowEmptyStrings = false)]
        [Display(Name = "Grupo")]
        [RegularExpression("A|B|C", ErrorMessage = "El grupo solo puede ser A, B o C")]
        public String GRUPO1;
    }

    public class MetaDatosHora_Clase
    {
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Hora de inicio es obligatoria", AllowEmptyStrings = false)]
        [Display(Name = "Hora Inicio", ShortName = "H. Inicio")]
        public DateTime HORA_INICIO;

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Hora de fin es obligatoria", AllowEmptyStrings = false)]
        [Display(Name = "Hora Fin", ShortName = "H. Fin")]
        public DateTime HORA_FIN;
    }
}
