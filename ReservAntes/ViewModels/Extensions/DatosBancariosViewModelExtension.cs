using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.ViewModels.Extensions
{
    public static class DatosBancariosViewModelExtension
    {
        public static DatosBancariosViewModel Map(this DatosBancarios value)
        {
            return new DatosBancariosViewModel
            {
                Id = value.Id,
                CBU=value.CBU,
                NumeroCuenta=value.NumeroCuenta

            };
        }
        public static DatosBancarios Map(this DatosBancariosViewModel model, DatosBancarios entity = null)
        {
            var edit = entity != null;
            if (!edit)
            {
                entity = new DatosBancarios
                {
                    Id = model.Id
                };
            }
            entity.CBU = model.CBU;
            entity.NumeroCuenta = model.NumeroCuenta;

            return entity;

        }
    }
}