using ReservAntes.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(ReservaExtension))]
    public class ReservaViewModel : Reserva
    {
        public List<EstadoReserva> estadoS { get; set; }
        public List<Cliente> clientes { get; set; }
        public List<Restaurante> restaurantes { get; set; }
        public double total { get; set; }
    }
}