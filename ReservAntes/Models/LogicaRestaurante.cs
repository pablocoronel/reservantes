using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;


namespace ReservAntes.Models
{
    public class LogicaRestaurante
    {
        dbReservAntesEntities ctx = new dbReservAntesEntities();

        public void CrearPlato(Plato plate)
        {
            Plato NewPlato = new Plato();
            NewPlato.NombrePlato = plate.NombrePlato;
            NewPlato.NombrePlato = plate.Descripcion;
            NewPlato.Precio = plate.Precio;


            // IMAGEN

            if (plate.Foto != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    plate.Foto.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();

                    var context = new Models.db();
                    ctx.activofijoes.Add(new Models.activofijo()
                    {
                        FotoAF = array,
                    });
                    context.SaveChanges();
                }


                // -------------------------------------------


                ctx.Plato.Add(NewPlato);
            ctx.SaveChanges();
        }

        }
    }
}