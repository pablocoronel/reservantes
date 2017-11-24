using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Models
{
    public class LogicaDomicilio
    {
        dbReservantesEntities ctx = new dbReservantesEntities();

        public List<Provincia> GetProvincias()
        {
            var provincias = ctx.Provincia.ToList();
            return provincias;
        }
        public List<Partido> GetByProvinciaId(int provinciaId)
        {
            var partidos = ctx.Partido.Where(x => x.ProvinciaId == provinciaId).ToList();
            return partidos;
        }
        /*Filtra localidades por partidoId*/
        public List<Localidad> GetByDepartamentoId (int partidoId)
        {
            var localidades = ctx.Localidad.Where(x => x.PartidoId == partidoId).ToList();
            return localidades;
        }
        /*Crear o actualizar domicilio*/
        public void CreateOrUpdate (Domicilio domicilio)
        {
            using (var db = new dbReservantesEntities())
            {
                if (domicilio.Id != 0)
                {
                    var domicilioDb = db.Domicilio.SingleOrDefault(x => x.Id == domicilio.Id);
                    db.Entry(domicilioDb).CurrentValues.SetValues(domicilio);
                }
                else
                {
                    db.Domicilio.Add(domicilio);
                }

                db.SaveChanges();
            }
        }

        public Domicilio GetById(int domicilioId)
        {
            return ctx.Domicilio.FirstOrDefault(x => x.Id == domicilioId);
        }
    }
}