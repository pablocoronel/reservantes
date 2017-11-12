using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(RestauranteExtension))]
    public class RestauranteViewModel : Restaurante
    {
        public DomicilioViewModel domicilio { get; set; }
    }
}