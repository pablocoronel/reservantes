using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(PlatosElegidosExtension))]
    public class PlatosElegidosViewModel : PlatosElegidos
    {
        //public List<PlatoViewModel> platos { get; set; }
        public PlatoViewModel plato {get;set;}
        public double? total { get; set; }
    }
}