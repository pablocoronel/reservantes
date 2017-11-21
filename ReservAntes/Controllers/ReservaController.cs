using ReservAntes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    public class ReservaController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();

        ReservAntes.Models.LogicaReserva LogRes = new LogicaReserva();

        // GET: Reserva
         public ActionResult ReporteReservas()
        {
            var IdUsuario = this.Session["usuarioId"];

            var listaReservas = from r in ctx.Restaurante where IdUsuario == r.Usuario && r.Reserva == ctx.Reserva select r;

            //List < Reserva > MisReservasResto = LogRes.GetReservasPorRestaurante();

            return View(listaReservas);
        }

        public ActionResult ReservaHorarios()
        {
            return View();
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reserva/Create
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

        // GET: Reserva/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reserva/Edit/5
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

        // GET: Reserva/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reserva/Delete/5
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
