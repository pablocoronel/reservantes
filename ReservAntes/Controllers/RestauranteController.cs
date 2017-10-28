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

        Models.LogicaRestaurante LogRes = new Models.LogicaRestaurante();


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


                return View("Restaurante/Index");
            }

            return View("../Shared/Error");
        }


        

    }
}
