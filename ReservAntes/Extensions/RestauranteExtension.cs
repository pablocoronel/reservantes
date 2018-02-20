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

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50)]
        public string NombreComercial { get; set; }
        public Nullable<int> DatosBancariosId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string CUIT { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public Nullable<int> CantidadClientes { get; set; }

        public bool Habilitado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public HttpPostedFileBase Foto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public HttpPostedFileBase ConstAFIP { get; set; }

        public Nullable<int> NivelId { get; set; }
        public string Domicilio { get; set; }
        public Nullable<double> Latitud { get; set; }
        public Nullable<double> Longitud { get; set; }


    }
}