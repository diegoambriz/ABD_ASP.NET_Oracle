//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RelojChecador.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RETARDO
    {
        public long ID_RETARDO { get; set; }
        public long ID_REGISTRO_ASISTENCIA { get; set; }
        public decimal DURACION { get; set; }
    
        public virtual REGISTRO_ASISTENCIA REGISTRO_ASISTENCIA { get; set; }
    }
}
