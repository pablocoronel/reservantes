using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Models
{
    public class LogicaReserva
    {
        dbReservantesEntities ctx = new dbReservantesEntities();

        //public List<Reserva> GetReservasPorRestaurante(int RestoID)
        //{
        //    var ReservasRestaurante = from r in ctx.Restaurante where r.Equals(r.IdRestaurante) select r;
           

        //    //List < Reserva > ReservasRestaurante = new List<Reserva>();
        //    //ReservasRestaurante = ctx.Reserva.ToList();

        //    return ReservasRestaurante(r);
        //}



        public List<Reserva> GetByRestauranteId(int idUS)
        {
            using (var db = new dbReservantesEntities())
            {
                var reservasFiltradas = db.Reserva.Include("EstadoReserva").Include("Cliente").Where(x => x.RestauranteId == idUS).ToList();
                return reservasFiltradas;
            }
        }
        //Para filtrar por fechas
        //public List<Reserva> GetByFecha(DateTime fechaInicio,DateTime fechaFin,int restauranteId)
        //{
        //    using (var db = new dbReservantesEntities())
        //    {
        //        var reservasFiltradas = db.Reserva.Include("EstadoReserva").Include("Cliente")
        //            .Where(x => x.RestaurenteId == restauranteId
        //            && x.Fecha>=fechaInicio&&x.Fecha<=fechaFin).ToList();
        //        return reservasFiltradas;
        //    }
        //}
        public List<Reserva> GetByClienteID(int cliente)
        {
            using (var db = new dbReservantesEntities())
            {
                var reservasFiltradas = db.Reserva.Include("EstadoReserva").Include("Restaurante").Where(x => x.ClienteId == cliente).ToList();
                return reservasFiltradas;
            }
        }
    }
}