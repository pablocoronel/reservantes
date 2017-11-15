using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Models
{
    public class LogicaDatosBancarios
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        
        public void CreateOrupdate (DatosBancarios datosBancarios)
        {
            using (var db = new dbReservantesEntities())
            {
                if (datosBancarios.Id != 0)
                {
                    var datosBancariosDb = db.DatosBancarios.SingleOrDefault(x => x.Id == datosBancarios.Id);
                    db.Entry(datosBancariosDb).CurrentValues.SetValues(datosBancarios);
                }
                else
                {
                    db.DatosBancarios.Add(datosBancarios);
                }

                db.SaveChanges();
            }
        }

        public DatosBancarios GetById(int datosBancariosId)
        {
            return ctx.DatosBancarios.FirstOrDefault(x => x.Id==datosBancariosId);
        }
    }
}