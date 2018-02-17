using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using ReservAntes.Extensions.Enums;
using ReservAntes.Extensions;

namespace ReservAntes.Models
{
    public class LogicaUsuario
    {

        public List<TipoUsuario> GetTiposDeUs()
        {
            List<TipoUsuario> ListUsuario = new List<TipoUsuario>();
            int tipoUsuarioAdmin = (int)TipoUsuarioEnum.Administrador;
            ListUsuario = ctx.TipoUsuario.Where(x =>x.TipoUsuarioEnum != tipoUsuarioAdmin).ToList();

            return ListUsuario;
        }

        // Listado Usuarios

        public List<Usuario> GetUsuario()
        {
            List<Usuario> todosLosUsuarios = new List<Usuario>();
            todosLosUsuarios = ctx.Usuario.Where(x => x.Activo == true).ToList();

            return todosLosUsuarios;
        }

        // Eliminar Usuarios

        public void EliminarUs(int id)
        {
            var usuario = (from Usuario in ctx.Usuario where Usuario.Id == id select Usuario).FirstOrDefault();
            
            //Desactivar usuario
            usuario.Activo = false;

            //Desactivar el restaurante del usuario
            if (usuario.TipoUsuarioId == 3)
            {
                Restaurante restaurante = ctx.Restaurante.Where(x => x.IdUsuario == usuario.Id).FirstOrDefault();

                if (restaurante != null)
                {
                    restaurante.Habilitado = false;
                }
                
            }

            ctx.SaveChanges();
        }


        dbReservantesEntities ctx = new dbReservantesEntities();

        public void CrearUsuario (UsuarioExtension us)
        {
            Usuario usuario = new Usuario();
            usuario.Username = us.Username;
            usuario.Email = us.Email;
            usuario.Password = us.Password;
            usuario.TipoUsuarioId = us.TipoUsuarioId;
            usuario.Activo = true;
            usuario.FechaDeRegistro = DateTime.Now;

            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();

            //Crear restaurante si el usuario corresponde a un resto
            if (us.TipoUsuarioId == 3)
            {
                Restaurante restaurante = new Restaurante();
                restaurante.IdUsuario = usuario.Id;
                restaurante.NombreComercial = "por defecto";
                restaurante.CUIT = "00-00000000-0";
                restaurante.CantidadClientes = 0;
                restaurante.Habilitado = true;
                restaurante.NivelId = 1;
                
                ctx.Restaurante.Add(restaurante);
                ctx.SaveChanges();
            }           

        }

        //Login de usuarios
        public Usuario UsuarioIngresar(Usuario usuario)
        {
            return ctx.Usuario.FirstOrDefault(usu => usu.Username == usuario.Username && usu.Password == usuario.Password);
        }
    }
}