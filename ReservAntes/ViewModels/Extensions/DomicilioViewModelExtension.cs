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
                LocalidadId = value.LocalidadId,
                localidadDescripcion = value.Localidad.Descripcion,
                partidoDescripcion = value.Localidad.Partido.Descripcion,
                provinciaDescripcion=value.Localidad.Partido.Provincia.Descripcion,
                Id=value.Id,
                NumeroCalle=value.NumeroCalle,
                NombreCalle=value.NombreCalle,
                NumeroDpto=value.NumeroDpto,
                NumeroPiso=value.NumeroPiso,
                Longitud=value.Longitud,
                Latitud=value.Latitud,
                Ubicacion=value.Ubicacion
                
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
            entity.NumeroCalle = model.NumeroCalle;
            entity.LocalidadId = model.LocalidadId;
            entity.Ubicacion = model.Ubicacion;
            entity.Latitud = model.Latitud;
            entity.Longitud = model.Longitud;

            return entity;

        }
    }
}