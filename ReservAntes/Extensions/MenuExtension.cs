using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class MenuExtension
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int EstiloMenuId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int RestauranteId { get; set; }
    }
}