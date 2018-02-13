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
            
            return View("Index");
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
