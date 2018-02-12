using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(PlatosElegidosExtension))]
    public class PlatoElegidoViewModel 
    {
        public bool elegido { get; set; }
        public List<int> cantidadPlatos { get; set; }
        public int cantidad { get; set; }
    }
}