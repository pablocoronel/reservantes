using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class DatosBancariosExtension
    {
        //Pendiente para corregir tipos de datos
        [Required]
        [StringLength(22, ErrorMessage ="Número de cuenta contiene 20 caracteres")]
        public string NumeroCuenta { get; set; }
        [Required]
        [StringLength(20,ErrorMessage ="CBU contiene 20 caracteres")]
        public string CBU { get; set; }
    }
}