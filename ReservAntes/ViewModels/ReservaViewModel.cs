using ReservAntes.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservAntes.ViewModels
{
    [MetadataType(typeof(ReservaExtension))]
    public class ReservaViewModel : Reserva
    {
        public List<EstadoReserva> estados { get; set; }
        public List<int> cantidadMaxima { get; set; }
        public string restauranteNombre { get; set; }
        public List<PlatoViewModel> platos { get; set; }
        //public RestauranteViewModel restauranteElegido { get; set; }
        public List<PlatosElegidosViewModel> platosElegidosVm { get; set; }

    }
}