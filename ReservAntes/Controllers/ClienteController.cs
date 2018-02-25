using ReservAntes;
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


        public ActionResult EditCliente(int id)
        {
            return View();
        }
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
            int idUsuario;
            Int32.TryParse(Session["usuarioId"].ToString(), out idUsuario);
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
            reserva.Restaurante = resto;
            //RestauranteViewModel restauranteElegido = resto.Map();
            //reserva.restauranteElegido = restauranteElegido;

            //reserva.restauranteNombre = resto.NombreComercial;
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(idResto);
            List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
            //cantidad de platos
            var cantidadMaxima = restauranteServicio.GetById(idResto).CantidadClientes;

            List<int> cantidades = new List<int>();
            var cantidad = 0;
            for (cantidad = 0; cantidad <= cantidadMaxima; cantidad++)
            {
                cantidades.Add(cantidad);
            }
            foreach (Plato plato in listaDePlatos)
            {
                listadoPlatos.Add(plato.Map());
            }
            foreach (var platoVM in listadoPlatos)
            {
                platoVM.cantidadPlatos = cantidades;
                platoVM.cantidad = 0;
            }
            //reserva.restauranteElegido.listadoPlatos = new List<PlatoViewModel>(listadoPlatos);
            reserva.platos = listadoPlatos;
            //platosElegidos.platos=listadoPlatos;
            return View("Reserva", reserva);
        }

        [HttpGet]
        public ActionResult ReservaHora(int idResto)
        {
            List<string> Horarios = new List<string>();
            //*Horario mañana
            int horaInicial = 12;
            for (horaInicial = 12; horaInicial <= 14; horaInicial ++)
            {
                Horarios.Add(horaInicial.ToString() + ":00");
            
            }
            //*Horario nocturno
            int horaNoche = 20;
            for (horaNoche= 20; horaNoche <= 23; horaNoche++)
            {
                Horarios.Add(horaNoche.ToString() + ":00");

            }
            var cantidadMaxima = restauranteServicio.GetById(idResto).CantidadClientes;
            //Creo listado para elegir cantidad comensales
            List<int> comensales = new List<int>();
            var persona = 1;
            for (persona = 1; persona < cantidadMaxima; persona++)
            {
                comensales.Add(persona);
            }

            HorariosReservaViewModel horarioReserva = new HorariosReservaViewModel();
            horarioReserva.Horarios = Horarios;
            horarioReserva.RestoId = idResto;
            horarioReserva.cantidadMaxima = comensales;
            horarioReserva.comensales = 0;

            return View("ReservaHora", horarioReserva);
        }
        [HttpPost]
        public ActionResult ReservaHora(HorariosReservaViewModel horarioReserva)
        {
            CultureInfo enUS = new CultureInfo("en-US");

            if (horarioReserva.comensales != 0)

            {
                
                String fechaHoy = Convert.ToString(DateTime.Today);

                String fechaHora = fechaHoy + " " + horarioReserva + ":00";
                DateTime fechaHoraReserva = DateTime.ParseExact(fechaHora, "MM/dd/yyyy HH:mm:ss", enUS, DateTimeStyles.None);
                if (fechaHoraReserva.AddHours(1) > DateTime.Now)
                {
                    ModelState.AddModelError("Hora", "No puede reservar con menos de una hora de anticipación");
                    return View("ReservaHora", horarioReserva);
                }
                var consultarDisponibilidad = restauranteServicio.VerCantidadDeComensales(horarioReserva.RestoId,fechaHoraReserva);
                if (consultarDisponibilidad >= horarioReserva.comensales)
                {
                    var reserva = new ReservaViewModel();
                    reserva.CantidadComensales = horarioReserva.comensales;
                    reserva.RestauranteId = horarioReserva.RestoId;
                    var resto = restauranteServicio.GetById(horarioReserva.RestoId);
                    reserva.Restaurante = resto;
                    List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(horarioReserva.RestoId);
                    List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
                    //cantidad de platos
                    var cantidadMaxima = restauranteServicio.GetById(horarioReserva.RestoId).CantidadClientes;

                    List<int> cantidades = new List<int>();
                    var cantidad = 0;
                    for (cantidad = 0; cantidad <= cantidadMaxima; cantidad++)
                    {
                        cantidades.Add(cantidad);
                    }
                    foreach (Plato plato in listaDePlatos)
                    {
                        listadoPlatos.Add(plato.Map());
                    }
                    foreach (var platoVM in listadoPlatos)
                    {
                        platoVM.cantidadPlatos = cantidades;
                        platoVM.cantidad = 0;
                    }
                    reserva.platos = listadoPlatos;

                    return View("Reserva", reserva);
                }
                ModelState.AddModelError("Hora", "No hay disponibilidad en ese horario");
                return View("ReservaHora", horarioReserva);

            }

            ModelState.AddModelError("Comensales", "Debe elegir cantidad de comensales");
            return View("ReservaHora", horarioReserva);

        }
        [HttpPost]
        public ActionResult Reserva(ReservaViewModel reserva)
        {
            var platosElegidos = new List<PlatosElegidosViewModel>();
            var reservaRealizada = new Reserva();
            foreach (var plato in reserva.platos)
            {
                if (plato.cantidad != null && plato.cantidad != 0)
                {
                    var platoElegido = new PlatosElegidosViewModel();
                    platoElegido.PlatoId = plato.Id;
                    platoElegido.Cantidad = plato.cantidad;
                    platoElegido.nombrePlato = plato.NombrePlato;
                    platoElegido.subTotal = plato.cantidad * Convert.ToDouble(plato.Precio);
                    platosElegidos.Add(platoElegido);
                    reserva.total = reserva.total + platoElegido.subTotal;
                }
            }
            reserva.platosElegidosVm = platosElegidos;
            return View("Reservar", reserva);
        }


        /* Lista de platos del restaurante elegido restaurante */
        public ActionResult ListaPlatos(int idResto)
        {
            //var platosElegidos = new PlatosElegidosViewModel();
            var resto = new RestauranteViewModel();
            resto = restauranteServicio.GetById(idResto).Map();
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(idResto);
            //List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
            //foreach (Plato plato in listaDePlatos)
            //{
            //    listadoPlatos.Add(plato.Map());
            //}
            resto.Plato = listaDePlatos;
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
            foreach (var plato in reservaFinal.platosElegidosVm)
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
