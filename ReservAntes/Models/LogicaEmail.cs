using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ReservAntes.Models
{
    public class LogicaEmail
    {
        [Required, Display(Name = "Nombre")]
        public string NombreConsu { get; set; }
        [Required, Display(Name = "Email"), EmailAddress]
        public string EmailConsu { get; set; }
        [Required, Display(Name = "Mensaje")]
        public string MensajeConsu { get; set; }
    }
}