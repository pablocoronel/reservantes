﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.Web.Mvc;

namespace ReservAntes.Extensions
{
    public class UsuarioExtension
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Supera el Limite de Campos")]
        public string Username { get; set; }
            

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string RePass { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int TipoUsuarioId { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaDeRegistro { get; set; }
    }
}