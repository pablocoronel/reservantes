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
                IdRestaurante=value.IdRestaurante,
                IdUsuario=value.IdUsuario,
                DatosBancariosId=value.DatosBancariosId,
                CantClientes=value.CantClientes,
                CUIT=value.CUIT,
                RazonSocial=value.RazonSocial,
                Estado=value.Estado,
                Foto=value.Foto,
                DomicilioID=value.DomicilioID

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
            entity.CantClientes = model.CantClientes;
            entity.RazonSocial = model.RazonSocial;
            entity.IdUsuario = model.IdUsuario;
            entity.DomicilioID = model.DomicilioID;
            entity.CUIT = model.CUIT;
            entity.Estado = model.Estado;
            entity.Foto = model.Foto;
            entity.DatosBancariosId = model.DatosBancariosId;
            return entity;

        }
    }
}