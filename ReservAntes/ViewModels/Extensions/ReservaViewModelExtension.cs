using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels.Extensions
{
    public static class ReservaViewModelExtension
    {
        //public static ReservaViewModelExtension Map(this Reserva value)
        //{
        //    return new DatosBancariosViewModel
        //    {
        //        Id = value.Id,
        //        CBU=value.CBU,
        //        NumeroCuenta=value.NumeroCuenta

        //    };
        //}
        public static Reserva Map(this ReservaViewModel model, Reserva entity = null)
        {
            var edit = entity != null;
            if (!edit)
            {
                entity = new Reserva
                {
                    Id = model.Id
                };
            }
            entity.CantidadComensales = model.CantidadComensales;
            entity.ClienteId = model.ClienteId;
            entity.CodigoReserva = model.CodigoReserva;
            entity.EstadoReservaId = model.EstadoReservaId;
            entity.FechaHoraReserva = model.FechaHoraReserva;
            entity.MedioPagoId = model.MedioPagoId;
            entity.RestauranteId = model.RestauranteId;
            entity.Observaciones = model.Observaciones;
            
            return entity;

        }
    }
}