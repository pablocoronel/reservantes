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

        public List<Localidad> GetByDepartamentoId (int partidoId)
        {
            var localidades = ctx.Localidad.Where(x => x.PartidoId == partidoId).ToList();
            return localidades;
        }

    }
}