﻿using ReservAntes.Models;
using ReservAntes.ViewModels;
using ReservAntes.ViewModels.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    //[Authorize]
    [HandleError]
    public class RestauranteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();
        LogicaRestaurante LogRes = new LogicaRestaurante();
        LogicaReserva LogRsv = new LogicaReserva();
        private LogicaDomicilio domicilioServicio = new LogicaDomicilio();
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

        // GET: Restaurante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Restaurante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurante/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurante/Edit/5
        [HttpPost]
        public ActionResult EditRestoran(int id, FormCollection collection)
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

        // GET: Restaurante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurante/Delete/5
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
        public ActionResult CreatePlato(Plato plato)
        {
            //if (ModelState.IsValid)

            bool resultado = this.LogRes.CrearPlato(plato);

            ViewBag.Guardado = resultado;
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

           /* if (ViewData["ResultadoEliminarPlato"] != null)
            {
                ViewData["ResultadoEliminarPlato"] = "si";
            }
            */

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
            Plato plato = ctx.Plato.Find(idPlato);

            return View(plato);
        }

        
        [HttpPost]
        public ActionResult EditarPlato(Plato plato)
        {
            //if (ModelState.IsValid)
            //{
                bool resultado = LogRes.EditarPlato(plato);

                if (resultado)
                {
                    ViewBag.Guardado = true;
                }
            //}

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
        public ActionResult RestoPerfil(RestauranteViewModel restaurante)
        {
            
            if (restaurante.IdRestaurante == 0)
            {
                restaurante.Habilitado =false;
                restaurante.IdUsuario = Convert.ToInt32(Session["usuarioId"]);
            }
            LogRes.CreateOrUpdate(restaurante.Map());
            //Paso a la vista de domicilio
            var domicilio = CargarListadosDomicilio();

            if (restaurante.DomicilioId != null)
            {
                var domicilioResto = domicilioServicio.GetById(restaurante.DomicilioId.Value);
                domicilio = domicilioResto.Map();
            }
            return View("Domicilio", domicilio);
        }
        public ActionResult Domicilio()
        {
            var domicilio = CargarListadosDomicilio();
            return View("Domicilio", domicilio);
        }
        [HttpPost]
        public ActionResult Domicilio(DomicilioViewModel domicilio)
        {
            var IdUsuario = Session["usuarioId"];
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));

            //domicilio.Ubicacion = GeoPoint.CreatePoint(domicilio.latitud, domicilio.longitud);
            domicilioServicio.CreateOrUpdate(domicilio.Map());
            LogRes.ActualizaDomicilio(domicilio.Id, restaurante.IdRestaurante);

            //paso a la vista de datos bancarios
            var datoBancario = new DatosBancariosViewModel();
            if (restaurante.DatosBancariosId != null)
            {
                var bancoResto=datosBancariosServicio.GetById(restaurante.DatosBancariosId.Value);
                datoBancario = bancoResto.Map();
            }
            return View("DatosBancarios",datoBancario);

        }
        private DomicilioViewModel CargarListadosDomicilio()
        {
            var domicilioNuevo = new DomicilioViewModel();
            var provinciaListado = ctx.Provincia.ToList();
            var departamentoListado = ctx.Partido.ToList();
            var localidadListado = ctx.Localidad.ToList();
            domicilioNuevo.provincias = provinciaListado;
            domicilioNuevo.partidos = departamentoListado;
            domicilioNuevo.localidades = localidadListado;
            return (domicilioNuevo);
        }


        public ActionResult DatosBancarios()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DatosBancarios(DatosBancariosViewModel datosBancarios)
        {
            var IdUsuario = Session["usuarioId"];
            var restaurante = LogRes.GetByUserId(Convert.ToInt32(IdUsuario));
            datosBancariosServicio.CreateOrupdate(datosBancarios.Map());
            LogRes.ActualizaDatosBancarios(datosBancarios.Id, restaurante.IdRestaurante);
            return View("Index");
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
    }
}
