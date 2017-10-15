using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    public class ClienteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();

            
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cliente/Details/5
        public ActionResult DetalleCliente(int id)
        {

            return View();
        }


        // GET: Cliente/Edit/5
        public ActionResult EditCliente(int id)
        {
            return View();
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult EditCliente(int id, FormCollection collection)
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

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cliente/Delete/5
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


        // GET: Cliente/Delete/5
        public ActionResult ReservCliente()
        {
            return View();
        }
    }
}
