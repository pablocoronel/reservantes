using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    //[MetadataType(typeof(HorarioReservaExtension))]
    public class HorariosReservaViewModel
    {
        public List<string> Horarios { get; set; }
        public string hora { get; set; }
        public int RestoId { get; set; }
    }
}