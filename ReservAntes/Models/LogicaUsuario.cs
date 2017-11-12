using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using ReservAntes.Extensions.Enums;

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
            todosLosUsuarios = ctx.Usuario.ToList();

            return todosLosUsuarios;
        }

        // Eliminar Usuarios

        public void EliminarUs(int id)
        {
            var DeleteUsuario = (from Usuario in ctx.Usuario where Usuario.Id == id select Usuario).FirstOrDefault();
            ctx.Usuario.Remove(DeleteUsuario);
            ctx.SaveChanges();
        }


        dbReservantesEntities ctx = new dbReservantesEntities();

        public void CrearUsuario (Usuario us)
        {
            Usuario NewUsuario = new Usuario();
            Restaurante nuevoRestaurante = new Restaurante();
            NewUsuario = us;
            //NewUsuario.Username = us.Username;
            //NewUsuario.Password = us.Password;
            //NewUsuario.Email = us.Email;
            //NewUsuario.TipoUsuarioId = us.TipoUsuarioId;
            ctx.Usuario.Add(NewUsuario);
            ctx.SaveChanges();
            //int tipoUsuarioResto = (int)TipoUsuarioEnum.Restaurante;

            ////Crear el restaurante si eligio tipo de usuario restaurante
            //if (us.TipoUsuarioId == ctx.TipoUsuario.Where(x=>x.TipoUsuarioEnum==tipoUsuarioResto).FirstOrDefault().Id)
            //{
            //    var usuarioRecienCreado = ctx.Usuario.OrderByDescending(x => x.Id).FirstOrDefault();

            //    nuevoRestaurante.IdUsuario = NewUsuario.Id;
            //    nuevoRestaurante.RazonSocial = null;
            //    nuevoRestaurante.DatosBancariosId = null;
            //    nuevoRestaurante.CUIT = null;
            //    nuevoRestaurante.Foto = null;
            //    nuevoRestaurante.CantClientes = null;
            //    nuevoRestaurante.Estado = 0;

            //    ctx.Restaurante.Add(nuevoRestaurante);
            //}

            //ctx.SaveChanges();

        }

        //Login de usuarios
        public Usuario UsuarioIngresar(Usuario usuario)
        {
            return ctx.Usuario.FirstOrDefault(usu => usu.Username == usuario.Username && usu.Password == usuario.Password);
        }
    }
}