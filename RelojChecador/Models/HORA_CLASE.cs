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
    
    public partial class HORA_CLASE
    {
        public HORA_CLASE()
        {
            this.HORARIO = new HashSet<HORARIO>();
        }
    
        public long ID_HORA_CLASE { get; set; }
        public System.DateTime HORA_INICIO { get; set; }
        public System.DateTime HORA_FIN { get; set; }
    
        public virtual ICollection<HORARIO> HORARIO { get; set; }
    }
}
