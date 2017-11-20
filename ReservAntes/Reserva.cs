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
    
    public partial class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
        public string Observaciones { get; set; }
        public int EstadoReservaId { get; set; }
        public System.DateTime FechaHoraReserva { get; set; }
        public string CodigoReserva { get; set; }
        public System.DateTime FechaHoraOperacion { get; set; }
        public Nullable<int> MedioPagoId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual EstadoReserva EstadoReserva { get; set; }
        public virtual MedioPago MedioPago { get; set; }
        public virtual PlatosElegidos PlatosElegidos { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}
