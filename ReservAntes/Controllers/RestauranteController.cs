using ReservAntes.Models;
using ReservAntes.ViewModels;
using ReservAntes.ViewModels.Extensions;
using System;
using System.Collections.Generic;
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

            return View(MisReservasResto);
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


        // --------------------- CREAR PLATO -----------------------------

        public ActionResult CreatePlato()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlato(Plato id, HttpPostedFile imagen)
        {
            {
                if (ModelState.IsValid)

                    this.LogRes.CrearPlato(id);


                return View("Index");
            }

        }

        // --------------------------------------------------

       

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
