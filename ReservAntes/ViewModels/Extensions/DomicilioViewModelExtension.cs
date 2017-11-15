using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels.Extensions
{
    public static class DomicilioViewModelExtension
    {
        public static DomicilioViewModel Map (this Domicilio value)
        {
            return new DomicilioViewModel
            {
                Localidad = value.Localidad,
                Id=value.Id,
                NumeroCalle=value.NumeroCalle,
                NombreCalle=value.NombreCalle,
                NumeroDpto=value.NumeroDpto,
                NumeroPiso=value.NumeroPiso,
                
            };
        }
        public static Domicilio Map(this DomicilioViewModel model, Domicilio entity = null)
        {
            var edit = entity != null;
            if (!edit)
            {
                entity = new Domicilio
                {
                    Id = model.Id
                };
            }
            entity.NumeroPiso = model.NumeroPiso;
            entity.NumeroDpto = model.NumeroDpto;
            entity.NombreCalle = model.NombreCalle;
            entity.Localidad = model.Localidad;
            entity.Ubicacion = model.Ubicacion;

            return entity;

        }
    }
}