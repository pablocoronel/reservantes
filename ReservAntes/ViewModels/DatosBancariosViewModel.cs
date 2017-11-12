using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(DatosBancariosExtension))]
    public class DatosBancariosViewModel : DatosBancarios
    {
    }
}