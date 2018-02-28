using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Models
{
    public class LogicaCliente
    {

        dbReservantesEntities ctx = new dbReservantesEntities();


        //Listado de Clientes

        public List<Cliente> GetCliente()
        {
            List<Cliente> todosLosClientes = new List<Cliente>();
            todosLosClientes = ctx.Cliente.ToList();

            return todosLosClientes;
        }

        //----------------------------------------------------------------------------------

        public List<Plato> ListarPlatosDelRestaurante(int idResto)
        {
            List<Plato> ListPlato = (from plato in ctx.Plato
                                     where idResto == plato.RestauranteId && plato.Activo==true
                                     select plato).ToList();

            return ListPlato;
        }

        public Cliente GetByUserId(int userId)
        {
            return ctx.Cliente.FirstOrDefault(x => x.IdUsuario == userId);
        }

        public List<Reserva> GetReservasDelCliente(int usuarioID)
        {
            Cliente cli = ctx.Cliente.Where(x => x.IdUsuario == usuarioID).FirstOrDefault();

            return ctx.Reserva.Where(x => x.ClienteId == cli.IdCliente).ToList();
        }
    }

}