using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(DomicilioExtension))]
    public class DomicilioViewModel : Domicilio
    {
        public List<Provincia> provincias { get; set; }
        public List<Partido> partidos { get; set; }
        public List<Localidad> localidades { get; set; }
        //public long latitud { get; set; }
        //public long longitud {get;set;}
    }
}