using ReservAntes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;



namespace ReservAntes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            dbReservantesEntities ctx = new dbReservantesEntities();

          

            if(Session["usuarioTipo"] != null)
                {
                switch (Convert.ToInt32(Session["usuarioTipo"]))
                {
                    case 1:
                        Response.Redirect("/Admin/Index/");
                        break;

                    case 2:
                        Response.Redirect("/Cliente/EditCliente/");
                        break;

                    case 3:
                        Response.Redirect("/Restaurante/DetalleCliente/");
                        break;

                    default:
                        Response.Redirect("/Home/Index/");
                        break;
                }

            }
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACTO";

            return View();
        }
    }
}