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


        // GET: Cliente/Edit/5
        public ActionResult EditCliente(int id)
        {
            return View();
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult EditCliente(int id, Cliente editCl)
        {
            
                var IdUsuario = this.Session["usuarioId"];
                var nomUsuario = this.Session["usuarioNombre"];


                if (ModelState.IsValid)
                {

                    Cliente EditCliente = new Cliente();

                    EditCliente.IdUsuario = Convert.ToInt32(IdUsuario);
                    EditCliente.Nombre = editCl.Nombre;
                    EditCliente.Apellido = editCl.Apellido;

                    ctx.Cliente.Add(EditCliente);
                    ctx.SaveChanges();

                    return View("Index");
                }

                //this.LogRes.crearMenu(id);

                return View("../Shared/Error");
            
           
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


        public ActionResult CompletarCliente()
        {

            return View();
        }

        public ActionResult DetalleCliente(Cliente cl)
        {
            var IdUsuario = this.Session["usuarioId"];
            var nomUsuario = this.Session["usuarioNombre"];


            if (ModelState.IsValid)
            {

                Cliente newCliente = new Cliente();

                newCliente.IdUsuario = Convert.ToInt32(IdUsuario);
                newCliente.Nombre = cl.Nombre;
                newCliente.Apellido = cl.Apellido;

                ctx.Cliente.Add(newCliente);
                ctx.SaveChanges();

                return View("Index");
            }

            //this.LogRes.crearMenu(id);

            return View("../Shared/Error");
        }


    }
}
