using ReservAntes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    
    [HandleError]
    public class AdminController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        LogicaRestaurante LogResto = new LogicaRestaurante();
        LogicaCliente LogCliente = new LogicaCliente();
        LogicaUsuario LogUsuario = new LogicaUsuario();


        // GET: Admin
        public ActionResult Index()
        {
            ViewData["CantUsuarios"] = ctx.Usuario.Count();

            //var cantus = ctx.Usuario.Count();


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

        
        [HttpGet]
        public ActionResult SuspenderRestaurante(int idResto)
        {
            LogResto.SuspenderRestaurante(idResto);

            return RedirectToAction(actionName: "VerRestaurantes");
        }


    }
}