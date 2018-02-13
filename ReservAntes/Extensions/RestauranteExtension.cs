using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ReservAntes.Extensions
{
    public class RestauranteExtension
    {
        public int IdRestaurante { get; set; }

        public int IdUsuario { get; set; }
<<<<<<< HEAD
        public string NombreComercial { get; set; }
        public Nullable<int> DatosBancariosId { get; set; }
        public string CUIT { get; set; }
        public Nullable<int> CantidadClientes { get; set; }
        public bool Habilitado { get; set; }
        public byte[] Foto { get; set; }
        public byte[] ConstAFIP { get; set; }
        public Nullable<int> NivelId { get; set; }
        public string Domicilio { get; set; }
        public Nullable<double> Latitud { get; set; }
        public Nullable<double> Longitud { get; set; }
=======

        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //[StringLength(50)]
        //public string RazonSocial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string NombreComercial { get; set; }

        public int? DatosBancariosId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string CUIT { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CantidadClientes { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public bool Habilitado { get; set; }

        //public int? DomicilioId { get; set; }

        public int? NivelId { get; set; }

        public string Domicilio { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }
>>>>>>> 0f8bb1105b4e4abcba5cd2794220b9e9894c4757
    }
}