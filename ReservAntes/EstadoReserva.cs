//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservAntes
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstadoReserva
    {
        public EstadoReserva()
        {
            this.Reserva = new HashSet<Reserva>();
        }
    
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> EstadoReservaEnum { get; set; }
    
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
