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


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerRestaurantes()
        {
            List<Restaurante> restaurantes = LogResto.GetRestaurantes();
            return View(model: restaurantes);
        }

        [HttpGet]
        public ActionResult HabilitarRestaurante(int idResto)
        {
            LogResto.HabilitarRestaurante(idResto);

            return RedirectToAction(actionName: "VerRestaurantes");
        }
    }
}