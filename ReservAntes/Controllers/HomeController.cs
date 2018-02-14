using ReservAntes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Http.Results;

namespace ReservAntes.Controllers
{
    public class HomeController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        ReservAntes.Models.LogicaRestaurante LogiRes = new ReservAntes.Models.LogicaRestaurante();
        

         public ActionResult Index()
        {
                    

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

        public JsonResult GetAllLocation()
        {
            //var data = LogiRes.GetRestaurantesHabilitados();
            //return Json(data, JsonRequestBehavior.AllowGet);
            //return Json(query, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                Restaurantes= (from obj in ctx.Restaurante.Where(x => x.Habilitado == true) select new { Latitud = obj.Latitud, Longitud = obj.Longitud, NombreComercial = obj.NombreComercial})
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACTO";

            return View();
        }
    }
}