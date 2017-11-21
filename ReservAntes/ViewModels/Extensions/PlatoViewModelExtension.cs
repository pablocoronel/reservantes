using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels.Extensions
{
    public static class PlatoViewModelExtension
    {
        public static PlatoViewModel Map(this Plato value)
        {
            return new PlatoViewModel
            {

                Id = value.Id,
                RestauranteId = value.RestauranteId,
                NombrePlato = value.NombrePlato,
                Foto = value.Foto,
                Descripcion = value.Descripcion,
                Precio = value.Precio
            };
        }
        public static Plato Map(this PlatoViewModel model, Plato entity = null)
        {
            var edit = entity != null;
            if (!edit)
            {
                entity = new Plato
                {
                    Id = model.Id
                };
            }
            entity.NombrePlato = model.NombrePlato;
            entity.RestauranteId = model.RestauranteId;
            entity.Precio = model.Precio;
            entity.Descripcion = model.Descripcion;
            entity.Foto = model.Foto;

            return entity;

        }
    }
}