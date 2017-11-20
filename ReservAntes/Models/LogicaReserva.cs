using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservAntes.Models
{
    public class LogicaReserva
    {
        public List<Reserva> GetByRestauranteId(int restauranteId)
        {
            using (var db = new dbReservantesEntities())
            {
                var reservasFiltradas = db.Reserva.Include("EstadoReserva").Include("Cliente").Where(x => x.RestauranteId == restauranteId).ToList();
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