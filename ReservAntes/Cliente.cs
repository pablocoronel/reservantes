//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservAntes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<int> DomicilioId { get; set; }
    
        public virtual Domicilio Domicilio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
