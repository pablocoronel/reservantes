using ReservAntes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservAntes.Extensions;
using System.Net.Mail;


namespace ReservAntes.Controllers
{
    public class UsuarioController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();

        LogicaUsuario LogUs = new LogicaUsuario();



        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult CreateUser()
        {
            if (Session["usuarioTipo"] != null)
            {
                if (Session["usuarioTipo"].ToString() != "1")
                {
                    Response.Redirect("../home/Index");
                }
                
            }

            ViewBag.ListUsuario = LogUs.GetTiposDeUs();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UsuarioExtension user)
        {

            {
                if (ModelState.IsValid)
                {
                    ViewBag.ListUsuario = LogUs.GetTiposDeUs();
                    this.LogUs.CrearUsuario(user);
                    ViewBag.Guardado = "Felicidades. Usted se ha registrado con exito." +
                        "Verifique por favor que ha recibido el mail de confirmacion.";

                    var message = new MailMessage();
                    message.From = new MailAddress("reservantesapp@gmail.com");
                    message.To.Add(user.Email);
                    message.Subject = "Registro ReservAntes";
                    message.Body = "<div class='container'>" +
                        " <h4>Felicidades " + user.Username  + " usted se ha registrado con Exito en ReservAntes </h4> " +
                        "En caso de cualquier detalle tecnico le pedimos por favor que se comunique con reservantesapp@gmail.com" +
                        "La contraseña que usted ha ingresado es: "+ user.Password +
                        "</div>";
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
                }

                ViewBag.ListUsuario = LogUs.GetTiposDeUs();
                return View();
                
            }

        }
         
        

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            this.LogUs.EliminarUs(id);

            return RedirectToAction("../Admin/VerUsuarios");
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       }
}


