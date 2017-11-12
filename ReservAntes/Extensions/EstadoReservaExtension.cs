using ReservAntes.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Extensions
{
    public class EstadoReservaExtension
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public EstadoReservaEnum EstadoReservaEnum { get; set; }

    }
}