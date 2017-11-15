using ReservAntes.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(ReservaExtension))]
    public class ReservaViewModel : Reserva
    {
        public List<EstadoReserva> estadoS { get; set; }
        public List<Cliente> clientes { get; set; }
        public List<Restaurante> restaurantes { get; set; }
    }
}