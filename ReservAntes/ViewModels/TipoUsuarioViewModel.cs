using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.Models
{
    [MetadataType(typeof(TipoUsuarioExtension))]
    public class TipoUsuarioViewModel : TipoUsuario
    {

    }
}