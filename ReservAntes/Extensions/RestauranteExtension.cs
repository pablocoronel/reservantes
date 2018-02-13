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

        public int IdUsuario { get; set; }

        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //[StringLength(50)]
        //public string RazonSocial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string NombreComercial { get; set; }

        public int? DatosBancariosId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string CUIT { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CantidadClientes { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public bool Habilitado { get; set; }

        //public int? DomicilioId { get; set; }

        public int? NivelId { get; set; }

        public string Domicilio { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }
    }
}