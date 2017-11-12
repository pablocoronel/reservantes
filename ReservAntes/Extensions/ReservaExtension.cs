using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class ReservaExtension
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int RestaurenteId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string Observaciones { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int EstadoReservaId { get; set; }
    }
}