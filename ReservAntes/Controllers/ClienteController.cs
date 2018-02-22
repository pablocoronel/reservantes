using ReservAntes.Models;
using ReservAntes.ViewModels;
using ReservAntes.ViewModels.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ReservAntes.Controllers
{
    //[Authorize]
    [HandleError]
    public class ClienteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();

        LogicaCliente LogCliente = new LogicaCliente();
        LogicaRestaurante restauranteServicio = new LogicaRestaurante();
        LogicaReserva logicaReserva = new LogicaReserva();
        // GET: Cliente
        public ActionResult Index()
        {
            List<RestauranteViewModel> listadoRestaurante = new List<RestauranteViewModel>();
         
            return View(listadoRestaurante);
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
 


        public ActionResult CompletarCliente()
        {

            return View();
        }

        [HttpGet]
        public ActionResult DetalleCliente(int id)
        {
            Cliente cliente = new Cliente();
            cliente = ctx.Cliente.Where(x => x.IdUsuario == id).FirstOrDefault();
            
            return View(cliente);
        }

        [HttpPost]
        public ActionResult DetalleCliente(Cliente cl)
        {
            Int32.TryParse(Session["usuarioId"].ToString(), out int idUsuario);
            Cliente cliente = ctx.Cliente.Where(x => x.IdUsuario == cl.IdUsuario).FirstOrDefault();

            //1 es admin
            if (cl.IdUsuario == idUsuario || Session["usuarioTipo"].ToString() == "1")
            {
                if (ModelState.IsValid)
                {
                    if (cliente != null)
                    {
                        cliente.Nombre = cl.Nombre;
                        cliente.Apellido = cl.Apellido;

                        ctx.SaveChanges();
                    }
                }

                return View(cliente);
            }

            return RedirectToRoute("Home/Index");
            
        }

        public ActionResult Reserva(int idResto)
        {
            //var platosElegidos = new PlatosElegidosViewModel();
            var reserva = new ReservaViewModel();
            reserva.RestauranteId = idResto;
            var resto = restauranteServicio.GetById(idResto);
            reserva.restauranteNombre = resto.NombreComercial;
            var cantidadMaxima = resto.CantidadClientes;
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(idResto);
            List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
            List<int> cantidades = new List<int>();
            var cantidad = 1;

            foreach (Plato plato in listaDePlatos)
            {
                listadoPlatos.Add(plato.Map());
            }
            foreach (var platoVM in listadoPlatos)
            {
                platoVM.cantidadPlatos = cantidades;
            }
            //Creo listado para elegir cantidad comensales
            List<int> comensales = new List<int>();
            var persona = 1;
            for (persona = 1; persona <= cantidadMaxima; persona++)
            {
                comensales.Add(persona);
            }
            for (cantidad = 1; cantidad < comensales.Max(); cantidad++)
            {
                cantidades.Add(cantidad);
            }
            reserva.cantidadMaxima = comensales;
            reserva.platos = listadoPlatos;
            //platosElegidos.platos=listadoPlatos;
            return View("Reserva", reserva);
        }

        [HttpPost]
        public ActionResult Reserva(ReservaViewModel reserva)
        {
            var platosElegidos = new List<PlatosElegidosViewModel>();
            var reservaRealizada = new Reserva();
            foreach(var plato in reserva.platos)
            {
                if (plato.cantidad!=null && plato.cantidad!=0)
                {
                    var platoElegido = new PlatosElegidosViewModel();
                    platoElegido.PlatoId = plato.Id;
                    platoElegido.Cantidad = plato.cantidad;
                    platoElegido.nombrePlato = plato.NombrePlato;
                    platoElegido.total = plato.cantidad * Convert.ToDouble(plato.Precio);
                    platosElegidos.Add(platoElegido);
                    reserva.total = reserva.total + platoElegido.total;
                }
            }
            reserva.platosElegidos = platosElegidos;
            return View ("Reservar",reserva);
        }

        /* Lista de platos del restaurante elegido restaurante */
        public ActionResult ListaPlatos(int idResto)
        {
            //var platosElegidos = new PlatosElegidosViewModel();
            var resto = new RestauranteViewModel();
            resto = restauranteServicio.GetById(idResto).Map();
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(idResto);
            List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
            foreach (Plato plato in listaDePlatos)
            {
                listadoPlatos.Add(plato.Map());
            }
            resto.listadoPlatos = listadoPlatos;
            //platosElegidos.platos=listadoPlatos;

            return View("ListaPlatos", resto);
        }

        [HttpPost]
        public ActionResult ConfirmarReserva(ReservaViewModel reservaFinal)
        {
            var IdUsuario = Session["usuarioId"];
            var cliente = LogCliente.GetByUserId(Convert.ToInt32(IdUsuario));
            var fechaHoy = DateTime.Now.Date;
            CultureInfo esAr = new CultureInfo("es-AR");
            String fecha = fechaHoy + " " + reservaFinal.hora + ":00" + ":00";
            reservaFinal.FechaHoraReserva = DateTime.ParseExact(fecha, "MM/dd/yyyy HH:mm:ss", esAr, DateTimeStyles.None);
            reservaFinal.MedioPagoId = 1;//Efectivo
            reservaFinal.EstadoReservaId = 1;
            reservaFinal.ClienteId = cliente.IdCliente;
            reservaFinal.CodigoReserva = Convert.ToString(reservaFinal.ClienteId) + Convert.ToString(reservaFinal.RestauranteId);
            var reserva = reservaFinal.Map();
            logicaReserva.CreateOrUpdate(reserva);
            var platosReservados = new List<PlatosElegidos>();
            foreach (var plato in reservaFinal.platosElegidos)
            {
                plato.ReservaId = reserva.Id;
                platosReservados.Add(plato.Map());
            }

            logicaReserva.CreatePlatos(platosReservados);
            //Estado RESERVADO
            var codigo = reserva.Id;
            return View("ConfirmarReserva", reservaFinal);



        }

        public ActionResult ReservaCliente()
        {
            List<Reserva> reservas = new List<Reserva>();

            if (Session["usuarioId"] != null)
            {
                Int32.TryParse(Session["usuarioId"].ToString(), out int usuarioId);

                reservas = LogCliente.GetReservasDelCliente(usuarioId);
            }
            

            return View(reservas);
        }
    }
}
