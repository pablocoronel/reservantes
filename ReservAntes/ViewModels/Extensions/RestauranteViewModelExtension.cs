using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels.Extensions
{
    public static class RestauranteViewModelExtension
    {
        public static RestauranteViewModel Map(this Restaurante value)
        {
            return new RestauranteViewModel
            {
                IdRestaurante = value.IdRestaurante,
                IdUsuario = value.IdUsuario,
                DatosBancariosId = value.DatosBancariosId,
                CantidadClientes = value.CantidadClientes,
                CUIT = value.CUIT,
                Habilitado = value.Habilitado,
                Foto = value.Foto,
                NombreComercial = value.NombreComercial,
                NivelId = value.NivelId,

            };
        }
        public static Restaurante Map(this RestauranteViewModel model, Restaurante entity = null)
        {
            var edit = entity != null;
            if (!edit)
            {
                entity = new Restaurante
                {
                    IdRestaurante = model.IdRestaurante
                };
            }
            entity.CantidadClientes = model.CantidadClientes;
            entity.IdUsuario = model.IdUsuario;
            entity.CUIT = model.CUIT;
            entity.Habilitado = model.Habilitado;
            entity.Foto = model.Foto;
            entity.DatosBancariosId = model.DatosBancariosId;
            entity.NombreComercial = model.NombreComercial;
            entity.NivelId = model.NivelId;
            return entity;

        }
    }
}