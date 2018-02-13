using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;



namespace ReservAntes.Models
{
    public class LogicaRestaurante

    {

        dbReservantesEntities ctx = new dbReservantesEntities();

        //Listado de restaurantes
        public List<Restaurante> GetRestaurantes()
        {
            List<Restaurante> todosLosRestaurantes = new List<Restaurante>(); 
            todosLosRestaurantes = ctx.Restaurante.ToList();

            return todosLosRestaurantes;
        }

        public List<Restaurante> GetRestaurantesHabilitados()
        {
            List<Restaurante> restaurantesHabilidatos = new List<Restaurante>();
            restaurantesHabilidatos = ctx.Restaurante.Where(x=>x.Habilitado==true).ToList();

            return restaurantesHabilidatos;
        }
        public List<Restaurante> GetRestaurantesNoHabilitados()
        {
            List<Restaurante> restaurantesNoHabilidatos = new List<Restaurante>();
            restaurantesNoHabilidatos = ctx.Restaurante.Where(x => x.Habilitado == false).ToList();

            return restaurantesNoHabilidatos;
        }

        //Listado de restaurantes
        //public List<Restaurante> GetRestaurantes()
        //{
        //    List<Restaurante> todosLosRestaurantes = new List<Restaurante>();
        //    todosLosRestaurantes = ctx.Restaurante.Include("Domicilio").ToList();

        //    return todosLosRestaurantes;
        //}

        //Listado de restaurantes
        public List<Restaurante> GetRestaurantesByLocalidad(int localidadId)
        {
            List<Restaurante> todosLosRestaurantes = new List<Restaurante>();
            todosLosRestaurantes = ctx.Restaurante.Include("Domicilio").ToList();

            return todosLosRestaurantes;
        }
        public void ActualizaEstado(bool estado, int restauranteId)
        {
            using (var db = new dbReservantesEntities())
            {
                var restauranteDb = db.Restaurante.SingleOrDefault(x => x.IdRestaurante == restauranteId);
                restauranteDb.Habilitado = estado;
                db.SaveChanges();
            }
        }
        public void ActualizaDomicilio(int domicilioId, int restauranteId)
        {
            using (var db = new dbReservantesEntities())
            {
                var restauranteDb = db.Restaurante.SingleOrDefault(x => x.IdRestaurante == restauranteId);
                db.SaveChanges();
            }
        }
        public void ActualizaDatosBancarios(int datosBancariosId, int restauranteId)
        {
            using (var db = new dbReservantesEntities())
            {
                var restauranteDb = db.Restaurante.SingleOrDefault(x => x.IdRestaurante == restauranteId);
                restauranteDb.DatosBancariosId = datosBancariosId;
                db.SaveChanges();
            }
        }
        public void CreateOrUpdate(Restaurante restaurante)
        {
            using (var db = new dbReservantesEntities())
            {
                if (restaurante.IdRestaurante != 0)
                {
                    var restauranteDb = db.Restaurante.SingleOrDefault(x => x.IdRestaurante == restaurante.IdRestaurante);

                    restauranteDb.CantidadClientes = restaurante.CantidadClientes;
                    restaurante.CUIT = restaurante.CUIT;
                }
                else
                {
                    db.Restaurante.Add(restaurante);
                }

                db.SaveChanges();
            }
        }
        //Habilitar el restaurante
        public void HabilitarRestaurante(int idresto)
        {
            Restaurante restaurante = ctx.Restaurante.FirstOrDefault(x => x.IdUsuario == idresto);
            // 0 => NO habilitado | 1 => Habilitado
            restaurante.Habilitado = true;

            ctx.SaveChanges();
        }


      

        public void CrearPlato(Plato plate)
        {

            //Plato NewPlato = new Plato();
            //NewPlato.NombrePlato = plate.NombrePlato;
            //NewPlato.NombrePlato = plate.Descripcion;
            //NewPlato.Precio = plate.Precio;


            //// IMAGEN

            //if (plate.Foto.ContentLength > 0)
            //{
            //    HttpPostedFileBase file = plate.Foto;
            //    string nombrePelicula = plate.NombrePlato;
            //    string nombreImagen = string.Concat(nombrePelicula, Path.GetExtension(file.FileName));

            //    string carpetaImagenes = System.Configuration.ConfigurationManager.AppSettings["CarpetaImagenes"];
            //    string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaImagenes;

            //    //si no exise la carpeta, la creamos
            //    if (!System.IO.Directory.Exists(pathDestino))
            //    {
            //        System.IO.Directory.CreateDirectory(pathDestino);
            //    }

            //    // guardar
            //    file.SaveAs(string.Concat(pathDestino, nombreImagen));

            //    //ruta de la imagen
            //    string rutaImagen = string.Concat(carpetaImagenes, nombreImagen);

            //    NewPlato.Foto = rutaImagen;


            //    // -------------------------------------------


            //    ctx.Plato.Add(NewPlato);
            //    ctx.SaveChanges();
            //}

        }

        // ----------------- Platos ---------------------


        public List<Plato> GetPlato(int idResto)
        {
            List<Plato> ListPlato = (from m in ctx.Plato
                                   where m.RestauranteId == idResto
                                     select m).ToList();

            return ListPlato;
        }

        public Restaurante GetById(int idResto)
        {
            return ctx.Restaurante.FirstOrDefault(x => x.IdRestaurante == idResto);
        }

        // -------------------------------------




        public Restaurante GetByUserId(int idUsuario)
        {
            return ctx.Restaurante.FirstOrDefault(x => x.IdUsuario == idUsuario);
        }




        // -------------------------------------

    }
}
