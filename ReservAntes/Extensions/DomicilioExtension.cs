using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Spatial;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class DomicilioExtension
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string NombreCalle { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int NumeroCalle { get; set; }
        public int NumeroPiso { get; set; }
        public int NumeroDpto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int LocalidadId { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public DbGeography Ubicacion { get; set; }
    }
}