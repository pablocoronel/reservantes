using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;


namespace ReservAntes.Models
{
    public class LogicaReserva
    {
        dbReservantesEntities ctx = new dbReservantesEntities();

        public List<Reserva> GetByRestauranteId(int idUS)
        {
            using (var db = new dbReservantesEntities())
            {
                var reservasFiltradas = db.Reserva.Include("EstadoReserva").Include("Cliente").Where(x => x.RestauranteId == idUS).ToList();
                return reservasFiltradas;
            }
        }
        public List<Reserva> GetByUsuarioId(int usuarioId)
        {
            using (var db = new dbReservantesEntities())
            {
                var restauranteId = db.Restaurante.FirstOrDefault(x => x.IdUsuario == usuarioId).IdRestaurante;
                var reservasFiltradas = db.Reserva.Include("EstadoReserva").Include("Cliente").Where(x => x.RestauranteId == restauranteId).ToList();
                return reservasFiltradas;
            }
        }

        public List<Reserva> GetByUsuarioIdFechaActual(int usuarioId)
        {
            using (var db = new dbReservantesEntities())
            {
                var restauranteId = db.Restaurante.FirstOrDefault(x => x.IdUsuario == usuarioId).IdRestaurante;
                var reservasFiltradasXFecha = db.Reserva.Include("EstadoReserva").Include("Cliente").Where(x => x.RestauranteId == restauranteId && x.FechaHoraReserva == DateTime.Today).ToList();



                return reservasFiltradasXFecha;
            }
        }

        
        public List<Reserva> FiltroReservas(int id, DateTime fechaFil)
        {

            List<Reserva> freservas = (from r in ctx.Reserva
                                       where r.RestauranteId == id && r.FechaHoraReserva == fechaFil
                                       select r).ToList();

            return freservas;
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
        public void CreateOrUpdate(Reserva reserva)
        {
            using (var db = new dbReservantesEntities())
            {

                db.Reserva.Add(reserva);
                db.SaveChanges();
            }
        }
        public void CreatePlatos(List<PlatosElegidos> platosElegidos)
        {
            using (var db = new dbReservantesEntities())
            {
                foreach (var item in platosElegidos)
                {
                    db.PlatosElegidos.Add(item);

                }
                db.SaveChanges();
            }
        }
    }
}