using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class UsuarioExtension
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int TipoUsuarioId { get; set; }
    }
}