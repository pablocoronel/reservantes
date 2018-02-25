using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class HorarioReservaExtension
    {
        [Required(ErrorMessage = "Elija Hora")]
        public string hora { get; set; }
        [Required(ErrorMessage = "Elija cantidad de personas que irán")]
        public int comensales { get; set; }

    }
}