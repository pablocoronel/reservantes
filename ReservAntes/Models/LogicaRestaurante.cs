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


       public List<EstiloMenu> GetEstilosMenus()
        {
            List<EstiloMenu> ListTiposMenus = new List<EstiloMenu>();
            ListTiposMenus = ctx.EstiloMenu.ToList();

            return ListTiposMenus;
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


        public List<Plato> GetPlato(int idMenu)
        {
            List<Plato> ListPlato = (from m in ctx.Plato
                                   where m.MenuId == idMenu
                                     select m).ToList();

            return ListPlato;
        }

        // -------------------------------------


        // ----------------- Menu ---------------------

       

        public List<Menu> GetMenu(int idMenu)
        {
            List<Menu> ListMenu = (from m in ctx.Menu
                                     where m.Id == idMenu
                                   select m).ToList();

            return ListMenu;
        }

       

        public void crearMenu(Menu menu)
        {
            Menu newMenu = new Menu();

            

            newMenu.Descripcion = menu.Descripcion;
            newMenu.EstiloMenu = menu.EstiloMenu;

            ctx.Menu.Add(newMenu);
            ctx.SaveChanges();
 
        }


        // -------------------------------------
    }
}
