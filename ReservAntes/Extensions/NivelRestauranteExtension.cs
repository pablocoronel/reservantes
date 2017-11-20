using ReservAntes.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class NivelRestauranteExtension
    {
        public decimal? Precio { get; set; }
        public string Descripcion { get; set; }
        public NivelRestauranteEnum EnumValue { get; set; }
    }
}