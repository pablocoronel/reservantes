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

    }

}