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
        public string nombrePlato {get;set;}
        public double? subTotal { get; set; }
    }
}