using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelojChecador.Models
{
    public class AsistenciaModel
    {
        [Required(ErrorMessage = "Debe seleccionar un profesor")]
        [Display(Name="Profesor")]
        public int idProfesor { get; set; }
        [Required(ErrorMessage="La firma es obligatoria")]
        [StringLength(20, ErrorMessage="El tamaño máximo son 20 caracteres")]
        [Display(Name="Firma")]
        public String firma { get; set; }

        public AsistenciaModel()
        {

        }
    }
}
