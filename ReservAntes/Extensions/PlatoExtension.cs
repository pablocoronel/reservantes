using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class PlatoExtension
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string NombrePlato { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal? Precio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Foto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int MenuId { get; set; }
    }
}