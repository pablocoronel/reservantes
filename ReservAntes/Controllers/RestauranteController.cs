using CapaServicio.Servicios;
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
    public class RestauranteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();
        LogicaRestaurante LogRes = new LogicaRestaurante();
        private LogicaDomicilio domicilioServicio = new LogicaDomicilio();


        // GET: Restaurante
        public ActionResult Index()
        {
            return View();
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

        // --------------------- MENU -------------------------------------------------------------------------------

        public ActionResult MiMenu()
        {
            return View();
        }

        public ActionResult CreateMenu()
        {

            ViewBag.ListTiposMenus = LogRes.GetEstilosMenus();
            return View();
        }

        [HttpPost]
        public ActionResult CreateMenu(Menu us)
        {

            ViewBag.ListTiposMenus = LogRes.GetEstilosMenus();

            var IdUsuario = this.Session["usuarioId"];
            var nomUsuario = this.Session["usuarioNombre"];


            if (ModelState.IsValid)
            {

            Menu newMenu = new Menu();

            newMenu.RestauranteId = Convert.ToInt32(IdUsuario);
            newMenu.Descripcion = us.Descripcion;
            newMenu.EstiloMenuId = us.EstiloMenuId;

            ctx.Menu.Add(newMenu);
            ctx.SaveChanges();

                return View("Index");
            }

            //this.LogRes.crearMenu(id);
            
            return View("../Shared/Error");
        }

        // ----------------------------------------------------------------------------------------------------



        // --------------------- Perfil Restoran -------------------------------------------------------------------------------


        public ActionResult RestoPerfil()
        {
            var restauranteNuevo = new RestauranteViewModel();
            var domicilioNuevo = new DomicilioViewModel();
            var provinciaListado = ctx.Provincia.ToList();
            var departamentoListado = ctx.Partido.ToList();
            var localidadListado = ctx.Localidad.ToList();
            domicilioNuevo.provincias = provinciaListado;
            domicilioNuevo.partidos = departamentoListado;
            domicilioNuevo.localidades = localidadListado;
            restauranteNuevo.domicilio = domicilioNuevo;
            return View(restauranteNuevo);
        }

        [HttpPost]
        public ActionResult RestoPerfil(RestauranteViewModel restaurante)
        {
            restaurante.domicilio.Ubicacion = GeoPoint.CreatePoint(restaurante.domicilio.latitud, restaurante.domicilio.longitud);
            restaurante.Estado = 0;
            var restoDomicilio = restaurante.domicilio;
            domicilioServicio.CreateOrUpdate(restoDomicilio.Map());
            restaurante.DomicilioID = restoDomicilio.Id;
            /*Buscar el usuario logueado*/
           // restaurante.IdUsuario = ;
            return View();
        }


        // ----------------------------------------------------------------------------------------------------


        // --------------------- Perfil Restoran -------------------------------------------------------------------------------


        public ActionResult DatosBancarios()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DatosBancarios(Restaurante id)
        {

            return View();
        }


        // ----------------------------------------------------------------------------------------------------



        

    }
}
