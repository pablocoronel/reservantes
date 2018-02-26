using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class ReservaExtension
    {
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int RestauranteId { get; set; }
        [StringLength(150)]
        public string Observaciones { get; set; }
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public int EstadoReservaId { get; set; }
        public DateTime FechaHoraReserva { get; set; }
        public string CodigoReserva { get; set; }
        public DateTime FechaHoraOperacion { get; set; }
        public int? MedioPagoId { get; set; }
        public double? Total { get; set; }
    }
}