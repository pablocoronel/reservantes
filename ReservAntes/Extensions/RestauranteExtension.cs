using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class RestauranteExtension
    {
        public int IdRestaurante { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string RazonSocial { get; set; }
        public int? DatosBancariosId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CUIT { get; set; }
        public byte[] Foto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CantClientes { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Estado { get; set; }
        public int? DomicilioID { get; set; }
    }
}