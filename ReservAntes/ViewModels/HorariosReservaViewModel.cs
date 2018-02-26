using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(HorarioReservaExtension))]
    public class HorariosReservaViewModel
    {
        //public List<HorariosModel> Horarios { get; set; }
        public List <string> Horarios { get; set; }
        public List<int> cantidadMaxima { get; set; }
        public int comensales { get; set; }
        public int RestoId { get; set; }
        public string hora { get; set; }
        //public int hora { get; set; }

    }

    public class HorariosModel
    {
        public HorariosEnum EnumValue { get; set; }
        public string Descripcion { get; set; }
    }
    public enum HorariosEnum

    { 
        Doce=12,
        Trece=13,
        Catorce=14,
        Veinte=20,
        Veintiuna=21,

    }


}