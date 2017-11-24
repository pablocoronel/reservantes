using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels.Extensions
{
    public static class PlatosElegidosViewModelExtension
    {

        public static PlatosElegidos Map(this PlatosElegidosViewModel model, PlatosElegidos entity = null)
        {

            entity.Cantidad = model.Cantidad;
            entity.ReservaId = model.ReservaId;
            entity.PlatoId = model.PlatoId;

            return entity;

        }

    }
}