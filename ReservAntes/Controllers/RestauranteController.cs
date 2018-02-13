using ReservAntes.Models;
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
        public ActionResult RestoPerfil(RestauranteViewModel restaurante)
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
           
            return View();
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
    }
}
