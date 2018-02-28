using mercadopago;
using ReservAntes;
using ReservAntes.Extensions.Enums;
using ReservAntes.Models;
using ReservAntes.ViewModels;
using ReservAntes.ViewModels.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Net.Mail;


namespace ReservAntes.Controllers
{
    //[Authorize]
    [HandleError]
    public class ClienteController : Controller
    {

        dbReservantesEntities ctx = new dbReservantesEntities();

        LogicaCliente LogCliente = new LogicaCliente();
        LogicaRestaurante restauranteServicio = new LogicaRestaurante();
        LogicaPlato logicaPlato = new LogicaPlato();
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
            HorariosReservaViewModel horarioReserva = InicializaHorarioReserva(idResto);
            return View("ReservaHora", horarioReserva);

        }

        [HttpGet]
        public ActionResult ReservaHora(int idResto)
        {
               
            HorariosReservaViewModel horarioReserva = InicializaHorarioReserva(idResto);
            return View("ReservaHora", horarioReserva);
        }
        [HttpPost]
        public ActionResult ReservaHora(HorariosReservaViewModel horarioReserva)
        {

            CultureInfo enUS = new CultureInfo("en-US");
            HorariosReservaViewModel nuevoHorarioReserva = InicializaHorarioReserva(horarioReserva.RestoId);
            if (horarioReserva.comensales != 0)

            {
                //string fechaHoy = DateTime.Today.ToShortDateString();
                //String fechaHora = fechaHoy + " " + horarioReserva.hora + ":00";
                //DateTime fechaHoraReserva = DateTime.ParseExact(fechaHora, "dd/MM/yyyy HH:mm:ss", enUS, DateTimeStyles.None);
                string fechaHoy = DateTime.Today.ToShortDateString();
                String fechaHora = fechaHoy + " " + horarioReserva.hora + ":00";
                DateTime fechaHoraReserva = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(horarioReserva.hora.Split(':')[0]), 0, 0);
                //Comentado temporalmente por la hora
                //if (fechaHoraReserva < DateTime.Now.AddHours(1))
                //{
                //    ModelState.AddModelError("Hora", "No puede reservar con menos de una hora de anticipación");
                //    return View("ReservaHora", nuevoHorarioReserva);
                //}
                //if (fechaHoraReserva < DateTime.Now)
                //{
                //    ModelState.AddModelError("Hora", "No puede reservar en ese horario");
                //    return View("ReservaHora", nuevoHorarioReserva);
                //}
                if (restauranteServicio.VerificaDisponibilidad(horarioReserva.RestoId, fechaHoraReserva, horarioReserva.comensales))
                {
                    var reserva = new ReservaViewModel();
                    reserva.CantidadComensales = horarioReserva.comensales;
                    reserva.FechaHoraReserva = fechaHoraReserva;
                    reserva.RestauranteId = horarioReserva.RestoId;
                    reserva.restauranteNombre = restauranteServicio.GetById(horarioReserva.RestoId).NombreComercial;
                    //var resto = restauranteServicio.GetById(horarioReserva.RestoId);
                    //reserva.Restaurante = resto;
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
                return View("ReservaHora", nuevoHorarioReserva);

            }

            ModelState.AddModelError("Comensales", "Debe elegir cantidad de comensales");
            return View("ReservaHora", nuevoHorarioReserva);
        }


        [HttpPost]
        public ActionResult Reserva(ReservaViewModel reserva)
        {
            var platosElegidos = new List<PlatosElegidosViewModel>();
            var reservaRealizada = new Reserva();
            reserva.Total = 0;
            var i = 0;
            for (i = 0; i < reserva.PlatoId.Count(); i++) 
            //foreach (var plato in reserva.platos)
            {
                //if (plato.cantidad != null && plato.cantidad != 0)
                if (reserva.PlatoCantidad[i] > 0)
                {
                    var platoElegido = new PlatosElegidosViewModel();
                    Plato plato=logicaPlato.GetById(reserva.PlatoId[i]);
                    platoElegido.PlatoId = reserva.PlatoId[i];
                    platoElegido.Cantidad = reserva.PlatoCantidad[i];
                    platoElegido.nombrePlato = plato.NombrePlato;
                    platoElegido.subTotal = platoElegido.Cantidad * Convert.ToDouble(plato.Precio);
                    platosElegidos.Add(platoElegido);
                    reserva.Total = reserva.Total + Convert.ToDecimal(platoElegido.subTotal);
                    //reserva.PlatosElegidos.Add(platoElegido.Map());
                }
            }
            if (platosElegidos.Count() > 0)
            {
                reserva.platosElegidosVm = platosElegidos;
                ModelState.AddModelError("Platos", "No eligió ningún plato");

            }
            reserva.restauranteNombre = restauranteServicio.GetById(reserva.RestauranteId).NombreComercial;
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(reserva.RestauranteId);
            var cantidadMaxima = restauranteServicio.GetById(reserva.RestauranteId).CantidadClientes;
            List<PlatoViewModel> listadoPlatos = new List<PlatoViewModel>();
            foreach (Plato plato in listaDePlatos)
            {
                listadoPlatos.Add(plato.Map());
            }
            List<int> cantidades = new List<int>();

            var cantidad = 0;
            for (cantidad = 0; cantidad <= cantidadMaxima; cantidad++)
            {
                cantidades.Add(cantidad);
            }
            foreach (var platoVM in listadoPlatos)
            {
                platoVM.cantidadPlatos = cantidades;
                platoVM.cantidad = 0;
            }

            reserva.platos = listadoPlatos;
            return View("ConfirmaReserva", reserva);
        }

        /* Lista de platos del restaurante elegido*/
        public ActionResult ListaPlatos(int idResto)
        {
            var resto = new RestauranteViewModel();
            resto = restauranteServicio.GetById(idResto).Map();
            List<Plato> listaDePlatos = LogCliente.ListarPlatosDelRestaurante(idResto);
            resto.Plato = listaDePlatos;
            return View("ListaPlatos", resto);
        }

        [HttpPost]
        public ActionResult ConfirmarReserva(ReservaViewModel reservaFinal)
        {
            var IdUsuario = Session["usuarioId"];
            
            var cliente = LogCliente.GetByUserId(Convert.ToInt32(IdUsuario));
            if (cliente == null)
            {
                return View("Index");
            }
            var i = 0;
            for (i = 0; i < reservaFinal.PlatoElegidoId.Count(); i++)
            {
                //if (plato.cantidad != null && plato.cantidad != 0)
                if (reservaFinal.PlatoElegidoCantidad[i] > 0)
                {
                    var platoElegido = new PlatosElegidos();
                    platoElegido.PlatoId = reservaFinal.PlatoElegidoId[i];
                    platoElegido.Cantidad = reservaFinal.PlatoElegidoCantidad[i];
                    reservaFinal.PlatosElegidos.Add(platoElegido);
               }
            }
            reservaFinal.FechaHoraReserva = reservaFinal.FechaHoraReserva;
            //Lo debe elegir el cliente, ahora guarda efectivo hasta que funcione MPago
            reservaFinal.MedioPagoId = 2;//Efectivo
            reservaFinal.EstadoReservaId = 1; // Reservado

            reservaFinal.ClienteId = cliente.IdCliente;
            reservaFinal.CodigoReserva = Convert.ToString(reservaFinal.FechaHoraReserva.Hour)+ Convert.ToString(reservaFinal.ClienteId) + Convert.ToString(reservaFinal.RestauranteId);
            var reserva = reservaFinal.Map();

            logicaReserva.CreateOrUpdate(reserva);

            //var platosReservados = new List<PlatosElegidos>();
            foreach (var platoElegido in reservaFinal.PlatosElegidos)
            {
                platoElegido.ReservaId = reserva.Id;
            }

            logicaReserva.CreatePlatos(reservaFinal.PlatosElegidos);
            //Estado RESERVADO
            //var codigo = reserva.Id;

<<<<<<< HEAD
                Cliente cli = ctx.Cliente.Where(x => x.IdCliente == reservaFinal.ClienteId).FirstOrDefault();

                Usuario us = ctx.Usuario.Where(x => x.Id == cli.IdUsuario).FirstOrDefault();

            Restaurante res = ctx.Restaurante.Where(x => x.IdRestaurante == reservaFinal.RestauranteId).FirstOrDefault();


            var message = new MailMessage();
            message.From = new MailAddress("reservantesapp@gmail.com");
            message.To.Add(us.Email);
            message.Subject = "ReservAntes -- RESERVA "+ res.NombreComercial +"";
            message.Body = "<div class='container'>" +
                " <h4>Felicidades " + cli.Nombre + " usted ha generado una reserva en " + res.NombreComercial + " </h4> " +
                "<p> Le comentamos que hasta que no confirme su reserva en su perfil no se hara efectiva la misma.</p>" +
                "<p></p>" +

                "<p> ReservAntes APP.</p>" +

                "</div>";
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            string sCorreoReservAntes = "reservantesapp@gmail.com";
            string sPsswordReservantes = "ReservAntes007";
            smtp.Credentials = new System.Net.NetworkCredential(sCorreoReservAntes, sPsswordReservantes);
            smtp.Send(message);



            return View("PagarReserva");
=======

            //return View("PagarReserva");
            return RedirectToAction("PagarReserva");
        }

        [HttpGet]
        public ActionResult PagarReserva()
        {
            MP mp = new MP("3569046944289967", "VKUe2kZa2BemjDp7vgNHu3ZTLStjlIhh");

            var producto = "Nombre Comercial";
            var precio = 500;


            var preference = mp.createPreference("{\"items\":[{\"title\":\"" + producto + "\",\"quantity\":1,\"currency_id\":\"ARS\",\"unit_price\":" + precio + "}]}");
            mp.sandboxMode(true);


            ViewBag.LinkMP = (((Hashtable)preference["response"])["sandbox_init_point"]);
           
            return View(preference);
>>>>>>> 46b82754829696bae0b2daa99eb80d4b5451c7c9
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

        private HorariosReservaViewModel InicializaHorarioReserva(int idResto)
        {
            List<string> Horarios = new List<string>();
            //*Horario mañana
            int horaInicial = 12;
            for (horaInicial = 12; horaInicial <= 14; horaInicial++)
            {
                Horarios.Add(horaInicial.ToString() + ":00");

            }
            //*Horario nocturno
            int horaNoche = 20;
            for (horaNoche = 20; horaNoche <= 23; horaNoche++)
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
            return horarioReserva;
        }
    }
}
