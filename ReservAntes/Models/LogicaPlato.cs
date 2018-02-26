using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Models
{
    public class LogicaPlato
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        public Plato GetById (int id)
        {
            var plato = ctx.Plato.FirstOrDefault(x => x.Id == id);
            return plato;
        }
    }
}