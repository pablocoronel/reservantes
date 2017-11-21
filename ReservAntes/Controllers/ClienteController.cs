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
    public class ClienteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();

        LogicaCliente LogCliente = new LogicaCliente();
            
        // GET: Cliente
        public ActionResult Index()
        {
            List<Restaurante> listaDeResto = LogCliente.GetRestaurantes();

            return View(model: listaDeResto);
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
            
                var IdUsuario = Session["usuarioId"];
                var nomUsuario = Session["usuarioNombre"];


                if (ModelState.IsValid)
                {

                
                    Cliente EditCliente = new Cliente();

                    EditCliente.IdUsuario = Convert.ToInt32(IdUsuario);
                    EditCliente.Nombre = editCl.Nombre;
                    EditCliente.Apellido = editCl.Apellido;
                    EditCliente.Domicilio = null;

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

        
        public ActionResult DetalleCliente()
        {
            return View();
        }

        [HttpPost]
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
                newCliente.Domicilio = null;
      

                ctx.Cliente.Add(newCliente);
                ctx.SaveChanges();

                return View("Index");
            }

            //this.LogRes.crearMenu(id);

            return View("../Shared/Error");
        }

        [HttpPost]
        public ActionResult PlatosElegidos(List<PlatoViewModel> platos)
        {
            var platosElegidos = new List<PlatosElegidosViewModel>();
            foreach(var plato in platos)
            {
                var platoElegido = new PlatosElegidosViewModel();
                platoElegido.PlatoId = plato.Id;
                platoElegido.Cantidad = plato.cantidad;
                platoElegido.total = plato.cantidad * Convert.ToDouble(plato.Precio);
                platosElegidos.Add(platoElegido);
                
            }
            //Falta Crear Reserva
            return View ("PlatosElegidos",platosElegidos);
        }

        /* Lista de platos de cada restaurante */
        public ActionResult ListaPlatos(int idResto)
        {
            //var platosElegidos = new PlatosElegidosViewModel();
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(idResto);
            List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
            List<int> cantidades = new List<int>();
            var cantidad = 1;
            for (cantidad = 1; cantidad < 20; cantidad++)
            {
                cantidades.Add(cantidad);
            }

            foreach (Plato plato in listaDePlatos)
            {
                listadoPlatos.Add(plato.Map());
            }
            foreach(var platoVM in listadoPlatos)
            {
                platoVM.cantidadPlatos = cantidades;
            }
            //platosElegidos.platos=listadoPlatos;
            return View("ListaPlatos", listadoPlatos);
        }
    }
}
