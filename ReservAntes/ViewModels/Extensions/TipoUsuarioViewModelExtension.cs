using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Models.Extensions
{
    public static class TipoUsuarioViewModelExtension
    {
        public static TipoUsuarioViewModel Map(this TipoUsuario value)
        {
            return new TipoUsuarioViewModel
            {
                Id=value.Id,
                Descripcion=value.Descripcion,
                TipoUsuarioEnum=value.TipoUsuarioEnum
            };
        }
    }
}