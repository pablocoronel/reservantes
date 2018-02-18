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
using System.Net.Mail;

namespace ReservAntes.Controllers
{
    public class HomeController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        ReservAntes.Models.LogicaRestaurante LogiRes = new ReservAntes.Models.LogicaRestaurante();
        

         public ActionResult Index()
        {
            /*
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
            */
            
            return View();
        }

        public JsonResult GetAllLocation()
        {
           
            return Json(new
            {
                Restaurantes= (from obj in ctx.Restaurante.Where(x => x.Habilitado == true) select new { Latitud = obj.Latitud, Longitud = obj.Longitud, IdRestaurante = obj.IdRestaurante, NombreComercial = obj.NombreComercial})
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Contact()
        {
            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(LogicaEmail model)
        {
            if (ModelState.IsValid)
            {
                
                var message = new MailMessage();
                message.From = new MailAddress(model.EmailConsu);
                message.To.Add("reservantesapp@gmail.com");
                message.Subject = "Contacto con ReservAntes -- :"+ model.EmailConsu;
                message.Body = model.MensajeConsu;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                string sCorreoReservAntes = "reservantesapp@gmail.com";
                string sPsswordReservantes = "ReservAntes007";
                smtp.Credentials = new System.Net.NetworkCredential(sCorreoReservAntes, sPsswordReservantes);
                smtp.Send(message);
                return RedirectToAction("Sent");
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}