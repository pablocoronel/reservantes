using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;

namespace ReservAntes.Models
{
    public class LogicaUsuario
    {

        public List<TipoUsuario> GetTiposDeUs()
        {
            List<TipoUsuario> ListUsuario = new List<TipoUsuario>();
            ListUsuario = ctx.TipoUsuario.ToList();

            return ListUsuario;
        }

        // Listado Usuarios

        public List<Usuario> GetUsuario()
        {
            List<Usuario> todosLosUsuarios = new List<Usuario>();
            todosLosUsuarios = ctx.Usuario.ToList();

            return todosLosUsuarios;
        }

        dbReservantesEntities ctx = new dbReservantesEntities();

        public void CrearUsuario (Usuario us)
        {
            Usuario NewUsuario = new Usuario();
            Restaurante nuevoRestaurante = new Restaurante();

            NewUsuario.Username = us.Username;
            NewUsuario.Password = us.Password;
            NewUsuario.Email = us.Email;
            NewUsuario.TipoUsuarioId = us.TipoUsuarioId;


            ctx.Usuario.Add(NewUsuario);

            //Crear el restaurante si eligio tipo de usuario restaurante
            if (us.TipoUsuarioId == 3)
            {
                var usuarioRecienCreado = ctx.Usuario.OrderByDescending(x => x.Id).FirstOrDefault();

                nuevoRestaurante.IdUsuario = usuarioRecienCreado.Id +1;
                nuevoRestaurante.RazonSocial = null;
                nuevoRestaurante.DatosBancariosId = null;
                nuevoRestaurante.CUIT = null;
                nuevoRestaurante.Foto = null;
                nuevoRestaurante.CantClientes = null;
                nuevoRestaurante.Estado = 0;

                ctx.Restaurante.Add(nuevoRestaurante);
            }

            ctx.SaveChanges();

        }

        //Login de usuarios
        public Usuario UsuarioIngresar(Usuario usuario)
        {
            return ctx.Usuario.FirstOrDefault(usu => usu.Username == usuario.Username && usu.Password == usuario.Password);
        }
    }
}