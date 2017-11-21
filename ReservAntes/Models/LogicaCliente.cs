﻿using System;
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

        //Listado de restaurantes
        public List<Restaurante> GetRestaurantes()
        {
            List<Restaurante> todosLosRestaurantes = new List<Restaurante>();
            todosLosRestaurantes = ctx.Restaurante.Include("Domicilio").Include("Localidad").ToList();

            return todosLosRestaurantes;
        }



        /* traer los platos del restaurante sin dar tantas vueltas*/
        public List<Plato> ListarPlatosDelRestaurante(int idResto)
        {
            List<Plato> ListPlato = (from plato in ctx.Plato
                                     where idResto == plato.RestauranteId
                                     select plato).ToList();

            return ListPlato;
        }
    }

}