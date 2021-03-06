﻿using ReservAntes.Models;
using ReservAntes.ViewModels;
using ReservAntes.ViewModels.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservAntes.Extensions;
using mercadopago;
using System.Net.Mail;

namespace ReservAntes.Controllers
{
    //[Authorize]
    [HandleError]
    public class RestauranteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();
        LogicaRestaurante LogRes = new LogicaRestaurante();
        LogicaReserva LogRsv = new LogicaReserva();
        private LogicaDatosBancarios datosBancariosServicio = new LogicaDatosBancarios();
        private LogicaReserva reservaServicio = new LogicaReserva();
        // GET: Restaurante


        public ActionResult Index()

        {
            var IdUsuario = this.Session["usuarioId"];
            int numID = Convert.ToInt32(IdUsuario);

            List<Reserva> MisReservasResto = LogRsv.GetByUsuarioIdFechaActual(numID);

            //Disponibilidad ACTUAL de lugares libres
            ViewBag.OcupacionActual = this.AsientosReservadosDelRestaurante(DateTime.Now);
            ViewBag.CapacidadTotal = this.CapacidadTotalDelRestaurante();
            
            return View(MisReservasResto);
        }

        [HttpPost]
        public ActionResult CapacidadPorFecha()

        {
            //Lugares disponibles por busqueda
            String[] fechaArray = Request.Form["buscarPorFecha"].Split('-');
            Int32.TryParse(fechaArray.ElementAt(0), out int anio);
            Int32.TryParse(fechaArray.ElementAt(1), out int mes);
            Int32.TryParse(fechaArray.ElementAt(2), out int dia);

            String[] horaArray = Request.Form["buscarPorHora"].Split(':');
            Int32.TryParse(horaArray.ElementAt(0), out int hora);
            Int32.TryParse(horaArray.ElementAt(1), out int minuto);
            
            DateTime fechaHoraBuscada = new DateTime(anio, mes, dia, hora, minuto, 0);

            TempData["OcupacionPorFecha"] = this.AsientosReservadosDelRestaurante(fechaHoraBuscada);

            return RedirectToAction("Index");
        }

      

        // --------------------- PLATO -----------------------------

        //crear
        public ActionResult CreatePlato()
        {
            if (Session["usuarioId"] != null)
            {
                Int32.TryParse(Session["usuarioId"].ToString(), out int usuarioId);
                Restaurante restaurante = LogRes.GetByUserId(usuarioId);
                ViewBag.idResto = restaurante.IdRestaurante;
            }
            

            return View();
        }

        [HttpPost]
        public ActionResult CreatePlato(PlatoViewModel plato)
        {
            if (ModelState.IsValid)
            {
                bool resultado = this.LogRes.CrearPlato(plato);

                ViewBag.Guardado = resultado;
            }
            return View();
        }

        //listado de platos
        public ActionResult MiMenu()
        {
            List<Plato> platos = new List<Plato>();
            if (Session["usuarioId"] != null)
            {
                Int32.TryParse(Session["usuarioId"].ToString(), out int idUsuario);

                Restaurante restaurante = LogRes.GetByUserId(idUsuario);

                platos = LogRes.GetPlato(restaurante.IdRestaurante);
            }

            return View(platos);
        }

        //borrar platos
        [HttpGet]
        public ActionResult EliminarPlato(int idPlato)
        {
            bool resultado = LogRes.EliminarPlato(idPlato);

            if (resultado)
            {
                TempData["ResultadoEliminarPlato"] = "si";
            }


            return RedirectToAction("MiMenu");
        }


        //editar platos
        [HttpGet]
        public ActionResult EditarPlato(int idPlato)
        {
            Plato platoBuscado= ctx.Plato.Find(idPlato);

            PlatoViewModel plato = platoBuscado.Map();
            return View(plato);
        }

        
        [HttpPost]
        public ActionResult EditarPlato(PlatoViewModel plato)
        {
            if (ModelState.IsValid)
            {
                bool resultado = LogRes.EditarPlato(plato);

                if (resultado)
                {
                    ViewBag.Guardado = true;
                }
            }

            return View();
        }

        // --------------------------------------------------

        /* capacidad disponible del restaurante */
        public int AsientosReservadosDelRestaurante(DateTime fechaHora)
        {
            Int32.TryParse(Session["usuarioId"].ToString(), out int idUsuario);
            Restaurante restaurante = ctx.Restaurante.Where(x => x.IdUsuario == idUsuario).FirstOrDefault();
            
            return LogRes.VerCantidadDeComensales(restaurante.IdRestaurante, fechaHora);
        }

        /* capacidad total del restaurante */
        public int CapacidadTotalDelRestaurante()
        {
            Int32.TryParse(Session["usuarioId"].ToString(), out int idUsuario);
            Restaurante restaurante = ctx.Restaurante.Where(x => x.IdUsuario == idUsuario).FirstOrDefault();

            return restaurante.CantidadClientes.Value;
        }
       


        
        // --------------------- Perfil Restoran -------------------------------------------------------------------------------


        public ActionResult RestoPerfil()
        {
            var IdUsuario = Session["usuarioId"];
            var restauranteNuevo = new RestauranteViewModel();
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));
            if ( restaurante!= null)
            {
                restauranteNuevo = restaurante.Map();
            };
            return View(restauranteNuevo);
        }

        [HttpPost]
        public ActionResult RestoPerfil(RestauranteExtension restaurante)
        {
            
            var IdUsuario = Session["usuarioId"];
            if (restaurante == null)
            {
                throw new ArgumentNullException(nameof(restaurante));
            }

            if (restaurante.IdRestaurante == 0)
            {
                restaurante.Habilitado =false;
                restaurante.IdUsuario = Convert.ToInt32(Session["usuarioId"]);
            }

            
            var restauranteId = LogRes.GetByUserId(Convert.ToInt32(IdUsuario)).IdRestaurante;
            restaurante.IdRestaurante = restauranteId;

            this.LogRes.CreateOrUpdate(restaurante);
              

            
            return View();
        }

        //TRAER LAS IMAGENES 

        public FileContentResult getImgFoto(int id)
        {
            byte[] byteArrayFoto = ctx.Restaurante.Find(id).Foto;

            return byteArrayFoto != null
                ? new FileContentResult(byteArrayFoto, "image/jpeg/png")
                : null;


        }

        public FileContentResult getImgAFIP(int id)
        {
            byte[] byteArrayAFIP = ctx.Restaurante.Find(id).ConstAFIP;

            return byteArrayAFIP != null
                ? new FileContentResult(byteArrayAFIP, "image/jpeg/png")
                : null;
        }



        public ActionResult DatosBancarios()
        {
            var IdUsuario = Session["usuarioId"];
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));
            var datoBancario = new DatosBancariosViewModel();
            if (restaurante.DatosBancariosId != null)
            {
                var bancoResto = datosBancariosServicio.GetById(restaurante.DatosBancariosId.Value);
                datoBancario = bancoResto.Map();
            }
            return View("DatosBancarios", datoBancario);
        }

        [HttpPost]
        public ActionResult DatosBancarios(DatosBancariosViewModel datosBancarios)
        {
            var IdUsuario = Session["usuarioId"];
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));
            DatosBancarios datosBancariosDb = datosBancarios.Map();
            datosBancariosServicio.CreateOrupdate(datosBancariosDb);
            LogRes.ActualizaDatosBancarios(datosBancariosDb.Id, restaurante.IdRestaurante);
            //Todo: Agregar vista success
            return View();
        }




        [HttpGet]
        public ActionResult Reservas()
        {
            var IdUsuario = Session["usuarioId"];
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));
            var listado = new List<Reserva>();
            listado = reservaServicio.GetByRestauranteId(restaurante.IdRestaurante);
            if (listado == null || listado.Count() == 0)
                ModelState.AddModelError("SinReservas", "No se encontraron Reservas");
            return View("Reservas",listado);
        }


        public ActionResult OpcionesResto()
        {
            var IdUsuario = Session["usuarioId"];
            var restauranteNuevo = new RestauranteViewModel();
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));
            if (restaurante != null)
            {
                restauranteNuevo = restaurante.Map();
            };


            MP mp = new MP("3569046944289967", "VKUe2kZa2BemjDp7vgNHu3ZTLStjlIhh");

            return View(restauranteNuevo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpcionesResto(RestauranteViewModel restoDeBaja)
        {

            var IdUsuario = Session["usuarioId"];
            var restauranteNuevo = new RestauranteViewModel();
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));

            

            ViewBag.Guardado = "Se le envio un mail al administrador para dar de baja el servicio." +
                        "Le pedimos por favor que aguarde que nosotros nos contactaremos con usted.";


            var message = new MailMessage();
            message.From = new MailAddress("reservantesapp@gmail.com");
            message.To.Add("reservantesapp@gmail.com");
            message.Subject = "CANCELAR SUSCRIPCION";
            message.Body = "<div class='container'>" +
                " <h4>El Restoran " + restoDeBaja.NombreComercial  + " ha solicitado la baja del servicio </h4> " +
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

            return View();
        }
    }

}
