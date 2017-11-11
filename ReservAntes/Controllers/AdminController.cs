using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    public class AdminController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        Models.LogicaRestaurante LogResto = new Models.LogicaRestaurante();
        Models.LogicaCliente LogCliente = new Models.LogicaCliente();
        Models.LogicaUsuario LogUsuario = new Models.LogicaUsuario();




        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // Listado Restaurantes
        public ActionResult VerRestaurantes()
        {
            List<Restaurante> restaurantes = LogResto.GetRestaurantes();
            return View(model: restaurantes);
        }

        // Listado Clientes

        public ActionResult VerClientes()
        {
            List<Cliente> clientes = LogCliente.GetCliente();
            return View(clientes);
        }


        // Listado Usuarios

        public ActionResult VerUsuarios()
        {
            List<Usuario> usuarios = LogUsuario.GetUsuario();
            return View(model: usuarios);
        }
      

        [HttpGet]
        public ActionResult HabilitarRestaurante(int idResto)
        {
            LogResto.HabilitarRestaurante(idResto);

            return RedirectToAction(actionName: "VerRestaurantes");
        }

       


    }
}